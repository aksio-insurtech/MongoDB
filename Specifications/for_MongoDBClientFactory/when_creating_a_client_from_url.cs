// Copyright (c) Aksio Insurtech. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using MELT;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Aksio.MongoDB.for_MongoDBClientFactory;

public class when_creating_a_client_from_url : Specification
{
    ITestLoggerFactory _loggerFactory;
    MongoDBClientFactory _clientFactory;
    IMongoClient _mongoClient;
    MongoUrl _connectionString;

    void Establish()
    {
        _loggerFactory = TestLoggerFactory.Create();
        var logger = _loggerFactory.CreateLogger<MongoDBClientFactory>();
        
        _clientFactory = new(logger);

        _connectionString = new("mongodb+srv://testuser:testpassword@testserver.mongodb.net/collectionname");
    }

    void Because()
    {
        _mongoClient = _clientFactory.Create(_connectionString);
    }

    [Fact]
    void max_connection_poolsize_is_as_expected() => _mongoClient.Settings.MaxConnectionPoolSize.ShouldEqual(100);
}
