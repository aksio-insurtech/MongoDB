// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.MongoDB.Resilience.for_MongoCollectionInterceptor.when_intercepting;

public class cancelled_method : given.an_interceptor
{
    protected override string GetInvocationTargetMethod() => nameof(InvocationTarget.CancelledMethod);

    void Because() => interceptor.Intercept(invocation.Object);

    [Fact] void should_have_cancelled_task() => return_value.IsCanceled.ShouldBeTrue();
    [Fact] void should_have_freed_up_semaphore() => semaphore.CurrentCount.ShouldEqual(pool_size);
}
