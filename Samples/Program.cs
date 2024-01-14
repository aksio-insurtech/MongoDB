using Aksio.MongoDB;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Samples;

MongoDBDefaults.Initialize();
var loggerFactory = new LoggerFactory();
var factory = new MongoDBClientFactory(loggerFactory.CreateLogger<MongoDBClientFactory>());

BsonClassMap.RegisterClassMap<Blah>(cm =>
{
    cm.AutoMap();
    cm.MapField(_ => _.Foo).SetSerializer(BsonSerializer.LookupSerializer<object>());
    // cm.SetDiscriminator("Blah");
    // cm.MapField("_bar").SetElementName("bar");
});

// BsonClassMap.RegisterClassMap<Bar>(cm =>
// {
//     cm.AutoMap();
//     cm.SetDiscriminator(typeof(Bar).AssemblyQualifiedName);
// });

var client = factory.Create("mongodb://localhost:27017");
var db = client.GetDatabase("test");
db.DropCollection("blahs");
var collection = db.GetCollection<Blah>();

var b = new Blah("Hello", 42, new Bar("aasd", typeof(string)));
var doc = b.ToBsonDocument();

// collection.InsertOne(new Blah("Hello", 42, "Something"));
await collection.InsertOneAsync(new Blah("Hello", 42, new Bar("aasd", typeof(string))));
await collection.InsertOneAsync(new Blah("Hello", 42, new Foo("Cat", "Horse", "Bar")));
await collection.InsertOneAsync(new Blah("Hello", 42, new Foo("Cat", "Horse", new Bar("Something", typeof(string)))));

var query = await collection.FindAsync(_ => true);
query.ToList().ForEach(Console.WriteLine);
