using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DemoMvcMongoLab.Models
{
    public class Customer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}