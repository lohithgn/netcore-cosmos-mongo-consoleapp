using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.IO;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;

namespace CosmosMongoConsoleApp
{
    class Program
    {
        private const string DB_NAME = "familiesdb";
        private const string COLLECTION_NAME = "families";
        private const string MONGO_DB_CONNECTION_KEY = "MongoDBConnection";

        async static Task Main(string[] args)
        {
            //Build Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            //Connect to DB
            var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration[MONGO_DB_CONNECTION_KEY]));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings);
            var database = client.GetDatabase(DB_NAME);
            var collection = database.GetCollection<Family>(COLLECTION_NAME);

            //Insert Document
            await InsertDocument(collection);
            Console.WriteLine();
            //Search Documents
            await FindFamilies(collection);
            Console.WriteLine();
            //Update Document
            await UpdateFamilies(collection);
            Console.WriteLine();
            await FindFamilies(collection);
            Console.WriteLine();
            //Delete Document
            await DeleteFamilies(collection);
            Console.ReadLine();
        }


        private async static Task InsertDocument(IMongoCollection<Family> collection)
        {
            
            var family = new Family
            {
                Id = "AndersenFamily",
                LastName = "Andersen",
                Parents = new []
                {
                    new Parent { FirstName = "Thomas"},
                    new Parent { FirstName = "Mary Kay"},
                },
                Children = new []
                {
                    new Child { FirstName = "John", Gender = "Male", Grade = 7}
                },
                Pets = new []
                {
                    new Pet { GivenName = "Fluffy"}
                },
                Address = new Address
                {
                    Country = "USA",
                    State = "WA",
                    City = "Seattle"
                }
            };
            try
            {
                Console.WriteLine("Inserting Andersen Family");
                Console.WriteLine(JsonSerializer.Serialize(family));
                await collection.InsertOneAsync(family);
            }
            catch(MongoCommandException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Done !!!");
            }
        }

        private async static Task FindFamilies(IMongoCollection<Family> collection)
        {
            try
            {
                Console.WriteLine("Finding Andersen Family");
                var familyFilter = Builders<Family>.Filter.Empty;
                await collection
                    .Find(familyFilter)
                    .ForEachAsync(family => Console.WriteLine(JsonSerializer.Serialize(family)));
            }
            catch(MongoCommandException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Done !!!");
            }
        }

        private async static Task UpdateFamilies(IMongoCollection<Family> collection)
        {
            try
            {
                Console.WriteLine("Updating Andersen Family");
                var filter = Builders<Family>.Filter.Eq(family => family.Id, "AndersenFamily");
                var updateDefinition = Builders<Family>.Update
                                            .Set(u => u.Pets, new [] { 
                                                new Pet { GivenName = "Fluffy" },
                                                new Pet { GivenName = "Rocky" },
                                            });

                var families = await collection.UpdateOneAsync(filter, updateDefinition);
            }
            catch(MongoCommandException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Done !!!");
            }
        }

        private async static Task DeleteFamilies(IMongoCollection<Family> collection)
        {
            try
            {
                Console.WriteLine("Deleting Andersen Family");
                var filter = Builders<Family>.Filter.Eq(family => family.LastName, "Andersen");
                var familyDeleteResult = await collection.DeleteManyAsync(filter);
                if (familyDeleteResult.DeletedCount == 1)
                {
                    Console.WriteLine($"Anderson Family deleted");
                }
            }
            catch(MongoCommandException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Done !!!");
            }
        }
    }
}