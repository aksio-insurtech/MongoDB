// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Aksio.Reflection;
using Aksio.Types;

namespace Aksio.MongoDB;

/// <summary>
/// Represents the default artifacts for MongoDB.
/// </summary>
public class DefaultMongoDBArtifacts : IMongoDBArtifacts
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultMongoDBArtifacts"/> class.
    /// </summary>
    /// <param name="assemblies"><see cref="ICanProvideAssembliesForDiscovery"/> for discovering types.</param>
    public DefaultMongoDBArtifacts(ICanProvideAssembliesForDiscovery assemblies)
    {
        ClassMaps = assemblies.DefinedTypes.Where(_ => _.HasInterface(typeof(IBsonClassMapFor<>))).ToArray();
        ConventionPackFilters = assemblies.DefinedTypes.Where(_ => _.HasInterface(typeof(ICanFilterMongoDBConventionPacksForType))).ToArray();
    }

    /// <inheritdoc/>
    public IEnumerable<Type> ClassMaps { get; }

    /// <inheritdoc/>
    public IEnumerable<Type> ConventionPackFilters { get; }
}
