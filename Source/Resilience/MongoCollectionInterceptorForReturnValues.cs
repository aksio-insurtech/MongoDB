// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Castle.DynamicProxy;
using MongoDB.Driver;
using Polly;

namespace Aksio.MongoDB;

/// <summary>
/// Represents an interceptor for <see cref="IMongoCollection{TDocument}"/> for methods that returns a <see cref="Task{T}"/>.
/// </summary>
public class MongoCollectionInterceptorForReturnValues : IInterceptor
{
    readonly ResiliencePipeline _resiliencePipeline;
    readonly SemaphoreSlim _openConnectionSemaphore;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoCollectionInterceptorForReturnValues"/> class.
    /// </summary>
    /// <param name="resiliencePipeline">The <see cref="ResiliencePipeline"/> to use.</param>
    /// <param name="mongoClient"><see cref="IMongoClient"/> the interceptor is for.</param>
    /// <param name="openConnectionSemaphore">The <see cref="SemaphoreSlim"/> for keeping track of open connections.</param>
    public MongoCollectionInterceptorForReturnValues(
        ResiliencePipeline resiliencePipeline,
        IMongoClient mongoClient,
        SemaphoreSlim openConnectionSemaphore)
    {
        _resiliencePipeline = resiliencePipeline;
        _openConnectionSemaphore = openConnectionSemaphore;
    }

    /// <inheritdoc/>
    public void Intercept(IInvocation invocation)
    {
        Task returnTask = null!;
        MethodInfo setResultMethod = null!;
        MethodInfo setExceptionMethod = null!;
        MethodInfo setCanceledMethod = null!;

        var returnType = invocation.Method.ReturnType.GetGenericArguments()[0];
        var taskType = typeof(TaskCompletionSource<>).MakeGenericType(returnType);
        var tcs = Activator.CreateInstance(taskType)!;
        var tcsType = tcs.GetType();
        setResultMethod = tcsType.GetMethod(nameof(TaskCompletionSource<object>.SetResult))!;
        setExceptionMethod = tcsType.GetMethod(nameof(TaskCompletionSource<object>.SetException), new Type[] { typeof(Exception) })!;
        setCanceledMethod = tcsType.GetMethod(nameof(TaskCompletionSource<object>.SetCanceled), Array.Empty<Type>())!;
        returnTask = (tcsType.GetProperty(nameof(TaskCompletionSource<object>.Task))!.GetValue(tcs) as Task)!;

        invocation.ReturnValue = returnTask!;

#pragma warning disable CA2012 // Use ValueTasks correctly
        _resiliencePipeline.ExecuteAsync(async (_) =>
        {
            await _openConnectionSemaphore.WaitAsync(1000);
            try
            {
                var result = (invocation.Method.Invoke(invocation.InvocationTarget, invocation.Arguments) as Task)!;
#pragma warning disable CA1849 // Synchronous block in a Task returning method
#pragma warning disable CA2008 // Do not create tasks without passing a TaskScheduler
                result.ContinueWith(_ =>
                {
                    _openConnectionSemaphore.Release(1);
                    if (_.IsFaulted && _.Exception is not null)
                    {
                        setExceptionMethod.Invoke(tcs, new[] { _.Exception });
                    }
                    else if (_.IsCanceled)
                    {
                        setCanceledMethod.Invoke(tcs, Array.Empty<object>());
                    }
                    else if (_.IsCompletedSuccessfully)
                    {
                        var taskResult = result.GetType().GetProperty(nameof(Task<object>.Result))!.GetValue(result);
                        setResultMethod.Invoke(tcs, new[] { taskResult });
                    }
                }).GetAwaiter().GetResult();
#pragma warning restore CA2008 // Do not create tasks without passing a TaskScheduler
#pragma warning restore CA1849 // Synchronous block in a Task returning method
            }
            catch (Exception ex)
            {
                _openConnectionSemaphore.Release(1);
                setExceptionMethod.Invoke(tcs, new[] { ex });
                return ValueTask.FromException(ex);
            }

            return ValueTask.CompletedTask;
        });
#pragma warning restore CA2012 // Use ValueTasks correctly
    }
}
