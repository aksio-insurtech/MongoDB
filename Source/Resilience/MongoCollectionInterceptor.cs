// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Castle.DynamicProxy;
using MongoDB.Driver;
using Polly;

namespace Aksio.MongoDB;

/// <summary>
/// Represents an interceptor for <see cref="IMongoCollection{TDocument}"/>.
/// </summary>
public class MongoCollectionInterceptor : IInterceptor
{
    readonly ResiliencePipeline _resiliencePipeline;
    readonly SemaphoreSlim _openConnectionSemaphore;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoCollectionInterceptor"/> class.
    /// </summary>
    /// <param name="resiliencePipeline">The <see cref="ResiliencePipeline"/> to use.</param>
    /// <param name="mongoClient"><see cref="IMongoClient"/> the interceptor is for.</param>
    public MongoCollectionInterceptor(ResiliencePipeline resiliencePipeline, IMongoClient mongoClient)
    {
        _resiliencePipeline = resiliencePipeline;
        _openConnectionSemaphore = new SemaphoreSlim(mongoClient.Settings.MaxConnectionPoolSize / 2, mongoClient.Settings.MaxConnectionPoolSize / 2);
    }

    /// <inheritdoc/>
    public void Intercept(IInvocation invocation)
    {
        try
        {
            if (invocation.Method.ReturnType != typeof(Task))
            {
                invocation.Proceed();
            }
            else
            {
                invocation.ReturnValue = _resiliencePipeline.ExecuteAsync(async (_) =>
                {
                    await _openConnectionSemaphore.WaitAsync();
                    var task = invocation.Method.Invoke(invocation.InvocationTarget, invocation.Arguments) as Task;
                    await task!;
                    _openConnectionSemaphore.Release(1);
                }).AsTask();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}