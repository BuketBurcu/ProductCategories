using MongoDB.Bson;
using ProductCategories.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCategories.Data.Models
{
    public class ResponseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
    }
}
