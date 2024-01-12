// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Aksio.MongoDB.Resilience.for_MongoCollectionInterceptor;

public class InvocationTarget
{
    public const string ErrorMessage = "Something went wrong";

    public Task SuccessfulMethod() => Task.CompletedTask;
    public Task CancelledMethod() => Task.FromCanceled(new CancellationToken(true));
    public Task FaultedMethod() => Task.Run(() =>
    {
        throw new(ErrorMessage);
    });

    public Task<string> FaultedMethodWithoutTask() => throw new(ErrorMessage);
}
