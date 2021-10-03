using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductCategories.Data.Dtos
{
    public class ProductDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
