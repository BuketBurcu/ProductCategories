using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using ProductCategories.Data.Dtos;
using ProductCategories.DataAccess;
using System.Net;

namespace ProductCategories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public string GetCategory([FromQuery] string id)
        {
            using (MongoRepository<CategoryDto> repository = new MongoRepository<CategoryDto>())
            {
                var cartegory = repository.Get(x => x.Id.Equals(id));
                if (cartegory == null)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return $"{id} product is not found.";
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    return JsonConvert.SerializeObject(cartegory);
                }
            }
        }

        [HttpPost]
        public string AddCategory([FromBody] CategoryDto category)
        {
            using (MongoRepository<CategoryDto> repository = new MongoRepository<CategoryDto>())
            {
                repository.Add(category);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return $"{category.Id} product is added.";
            }
        }

        [HttpPut]
        public string SetCategory([FromBody] CategoryDto category)
        {
            using (MongoRepository<CategoryDto> repository = new MongoRepository<CategoryDto>())
            {

                repository.Update(x => x.Id.Equals(category.Id), category);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return JsonConvert.SerializeObject(category);
            }
        }

        [HttpDelete]
        public string DeleteCategory([FromQuery] string id)
        {
            using (MongoRepository<CategoryDto> repository = new MongoRepository<CategoryDto>())
            {
                repository.Delete(x => x.Id.Equals(ObjectId.Parse(id)));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return $"{id} product is deleted.";
            }
        }
    }
}