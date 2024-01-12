// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Aksio.MongoDB.Resilience.for_MongoCollectionInterceptorSelector.when_selecting_interceptors;

public class for_async_methods : given.an_interceptor_selector
{
    protected IEnumerable<MethodInfo> async_methods;
    int intercepted_methods;

    void Establish()
    {
        async_methods = typeof(IMongoCollection<BsonDocument>).GetMethods().Where(m => m.ReturnType == typeof(Task)).ToArray();
    }

    void Because() => intercepted_methods = async_methods.Count(methodInfo =>
    {
        var interceptors = selector.SelectInterceptors(typeof(IMongoCollection<BsonDocument>), methodInfo, Array.Empty<Castle.DynamicProxy.IInterceptor>());
        return interceptors.Length == 1 && interceptors[0] is MongoCollectionInterceptor;
    });

    [Fact] void should_have_the_mongo_collection_interceptor_for_all() => intercepted_methods.ShouldEqual(async_methods.Count());
}
