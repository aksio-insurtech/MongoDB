// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using Polly;

namespace Aksio.MongoDB.Resilience.for_MongoCollectionInterceptorSelector.when_selecting_interceptors;

public class for_sync_methods : Specification
{
    MongoCollectionInterceptorSelector selector;
    ResiliencePipeline resilience_pipeline;
    Mock<IMongoClient> mongo_client;
    IEnumerable<MethodInfo> sync_methods;
    MongoClientSettings settings;
    int intercepted_methods;

    void Establish()
    {
        resilience_pipeline = new ResiliencePipelineBuilder().Build();
        mongo_client = new();
        settings = new MongoClientSettings();
        mongo_client.SetupGet(_ => _.Settings).Returns(settings);
        selector = new MongoCollectionInterceptorSelector(resilience_pipeline, mongo_client.Object);

        sync_methods = typeof(IMongoCollection<BsonDocument>).GetMethods().Where(m => !m.ReturnType.IsAssignableTo(typeof(Task))).ToArray();
    }

    void Because() => intercepted_methods = sync_methods.Count(methodInfo =>
    {
        var interceptors = selector.SelectInterceptors(typeof(IMongoCollection<BsonDocument>), methodInfo, Array.Empty<Castle.DynamicProxy.IInterceptor>());
        return interceptors.Length == 0;
    });

    [Fact] void should_have_no_mongo_collection_interceptor_for_all() => intercepted_methods.ShouldEqual(sync_methods.Count());
}
