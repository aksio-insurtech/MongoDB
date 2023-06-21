// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Aksio.Types;

namespace Aksio.MongoDB;

/// <summary>
/// Represents the default artifacts for MongoDB.
/// </summary>
public class DefaultMongoDBArtifacts : IMongoDBArtifacts
{
    /// <inheritdoc/>
    public IEnumerable<Type> ClassMaps { get; }

    /// <inheritdoc/>
    public IEnumerable<Type> ConventionPackFilters { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultMongoDBArtifacts"/> class.
    /// </summary>
    /// <param name="types"><see cref="ITypes"/> for discovering types.</param>
    public DefaultMongoDBArtifacts(ITypes types)
    {
        ClassMaps = types.FindMultiple(typeof(IBsonClassMapFor<>));
        ConventionPackFilters = types.FindMultiple(typeof(ICanFilterMongoDBConventionPacksForType));
    }
}
