// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using Castle.DynamicProxy;
using MongoDB.Driver;
using Polly;

namespace Aksio.MongoDB;

/// <summary>
/// Represents a selector for <see cref="MongoDatabaseInterceptor"/>.
/// </summary>
public class MongoDatabaseInterceptorSelector : IInterceptorSelector
{
    readonly ProxyGenerator _proxyGenerator;
    readonly ResiliencePipeline _resiliencePipeline;
    readonly IMongoClient _mongoClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoDatabaseInterceptorSelector"/> class.
    /// </summary>
    /// <param name="proxyGenerator"><see cref="ProxyGenerator"/> for creating further proxies.</param>
    /// <param name="resiliencePipeline">The <see cref="ResiliencePipeline"/> to use.</param>
    /// <param name="mongoClient"><see cref="IMongoClient"/> to intercept.</param>
    public MongoDatabaseInterceptorSelector(
        ProxyGenerator proxyGenerator,
        ResiliencePipeline resiliencePipeline,
        IMongoClient mongoClient)
    {
        _proxyGenerator = proxyGenerator;
        _resiliencePipeline = resiliencePipeline;
        _mongoClient = mongoClient;
    }

    /// <inheritdoc/>
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        if (method.Name == nameof(IMongoDatabase.GetCollection))
        {
            return new[] { new MongoDatabaseInterceptor(_proxyGenerator, _resiliencePipeline, _mongoClient) };
        }
        return Array.Empty<IInterceptor>();
    }
}
