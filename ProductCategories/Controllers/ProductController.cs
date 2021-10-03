using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using ProductCategories.Data.Dtos;
using ProductCategories.Data.Models;
using ProductCategories.DataAccess;
using ProductCategory.RedisCache;
using System.Net;

namespace ProductCategories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public string GetProduct([FromQuery] string id)
        {
            var data = RedisIndexer.Intance.GetProductIndex(id);
            if (data == null)
            {
                using (MongoRepository<ProductDto> productRepository = new MongoRepository<ProductDto>())
                {
                    using (MongoRepository<CategoryDto> categoryRepository = new MongoRepository<CategoryDto>())
                    {
                        var product = productRepository.Get(x => x.Id.Equals(id));
                        if (product == null)
                        {
                            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            return null;
                        }
                        else
                        {
                            ResponseModel response = new ResponseModel()
                            {
                                Id = product.Id,
                                Category = categoryRepository.Get(x => x.Id.Equals(ObjectId.Parse(product.CategoryId))),
                                Currency = product.Currency,
                                Description = product.Description,
                                Name = product.Name,
                                Price = product.Price

                            };
                            data = JsonConvert.SerializeObject(response);
                            RedisIndexer.Intance.SetProduct(id, data);
                            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                            return data;
                        }
                    }
                }
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return data;
            }
        }

        [HttpPost]
        public string AddProduct([FromBody] ProductDto product)
        {
            using (MongoRepository<ProductDto> repository = new MongoRepository<ProductDto>())
            {
                repository.Add(product);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return $"{product.Id} product added.";
            }
        }

        [HttpPut]
        public string SetProduct([FromBody] ProductDto product)
        {
            using (MongoRepository<ProductDto> repository = new MongoRepository<ProductDto>())
            {
                repository.Update(x => x.Id.Equals(product.Id), product);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return JsonConvert.SerializeObject(product);
            }
        }

        [HttpDelete]
        public string DeleteProduct([FromQuery] string id)
        {
            using (MongoRepository<ProductDto> repository = new MongoRepository<ProductDto>())
            {
                repository.Delete(x => x.Id.Equals(ObjectId.Parse(id)));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                return $"{id} product deleted.";
            }
        }
    }
}
