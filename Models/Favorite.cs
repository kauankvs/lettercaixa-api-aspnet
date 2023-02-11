using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LettercaixaAPI.Models
{
    public class Favorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int ProfileId { get; set; }
        public int[] Movies { get; set; }
    }
}
