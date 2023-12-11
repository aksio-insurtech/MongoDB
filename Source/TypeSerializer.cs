// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Aksio.MongoDB;

/// <summary>
/// Represents a serializer for <see cref="Type"/>.
/// </summary>
public class TypeSerializer : SerializerBase<Type>
{
    /// <inheritdoc/>
    public override Type Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.GetCurrentBsonType();
        switch (bsonType)
        {
            case BsonType.String:
                return Type.GetType(context.Reader.ReadString()) ?? throw new InvalidOperationException("Could not deserialize type.");

            default:
                throw new NotSupportedException($"Cannot deserialize a {bsonType} to a {nameof(Type)}.");
        }
    }

    /// <inheritdoc/>
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Type value)
    {
        context.Writer.WriteString(value.GetTypeString());
    }
}