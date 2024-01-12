// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MongoDB.Driver;
using Polly;

namespace Aksio.MongoDB.Resilience.for_MongoCollectionInterceptorForReturnValue.given;

public abstract class an_interceptor : Specification
{
    protected const int pool_size = 10;
    protected ResiliencePipeline resilience_pipeline;
    protected Mock<IMongoClient> mongo_client;
    protected MongoClientSettings settings;
    protected MongoCollectionInterceptorForReturnValues interceptor;
    protected Mock<Castle.DynamicProxy.IInvocation> invocation;
    protected Task<string> return_value;
    protected InvocationTarget target;
    protected SemaphoreSlim semaphore;

    protected abstract string GetInvocationTargetMethod();


    void Establish()
    {
        resilience_pipeline = new ResiliencePipelineBuilder().Build();
        mongo_client = new();
        settings = new MongoClientSettings();
        mongo_client.SetupGet(_ => _.Settings).Returns(settings);

        semaphore = new SemaphoreSlim(pool_size, pool_size);

        interceptor = new(resilience_pipeline, mongo_client.Object, semaphore);

        invocation = new();
        invocation.SetupGet(_ => _.Method).Returns(typeof(InvocationTarget).GetMethod(GetInvocationTargetMethod())!);
        target = new();
        invocation.SetupGet(_ => _.InvocationTarget).Returns(target);
        invocation.SetupSet(_ => _.ReturnValue = It.IsAny<Task<string>>()).Callback((object? value) => return_value = value as Task<string>);
    }
}
