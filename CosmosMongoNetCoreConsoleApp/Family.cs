using MongoDB.Bson.Serialization.Attributes;

namespace CosmosMongoConsoleApp
{
    public class Family
    {
        [BsonElement("Id")]
        public string Id { get; set; }
        
        [BsonElement("LastName")]
        public string LastName { get; set; }
        
        [BsonElement("Parents")]
        public Parent[] Parents { get; set; }
        
        [BsonElement("Children")]
        public Child[] Children { get; set; }
        
        [BsonElement("Pets")]
        public Pet[] Pets { get; set; }
        
        [BsonElement("Address")]
        public Address Address { get; set; }
    }

    public class Address
    {
        [BsonElement("Country")]
        public string Country { get; set; }
        
        [BsonElement("State")]
        public string State { get; set; }
        
        [BsonElement("City")]
        public string City { get; set; }
    }

    public class Parent
    {
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
    }

    public class Child
    {
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        
        [BsonElement("Gender")]
        public string Gender { get; set; }
        
        [BsonElement("Grade")]
        public int Grade { get; set; }
    }

    public class Pet
    {
        [BsonElement("GivenName")]
        public string GivenName { get; set; }
    }
}
