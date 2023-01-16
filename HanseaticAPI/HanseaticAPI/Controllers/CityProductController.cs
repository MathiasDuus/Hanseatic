using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/city_product")]
    [ApiController]
    public class CityProductController : ControllerBase
    {
        private static List<CityProduct> city_products = new List<CityProduct>
            {
                new CityProduct
                {
                    Id = 1,
                    city_id= 1,
                    product_type = 1,
                    desired_amount = 22,
                    actual_amount = 23,
                    sell_price = 23,
                    buy_price = 33,
                    min_fluctation = 0.95,
                    max_fluctation = 1.54
                },
                new CityProduct
                {
                    Id = 2,
                    city_id= 1,
                    product_type = 2,
                    desired_amount = 1,
                    actual_amount = 1,
                    sell_price = 1,
                    buy_price = 33,
                    min_fluctation = 0.95,
                    max_fluctation = 1.54
                }
            };

        [HttpGet]
        public async Task<ActionResult<List<CityProduct>>> Get()
        {
            return Ok(city_products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CityProduct>>> Get(int id)
        {
            var product = city_products.Find(p => p.Id == id);
            if (product == null)
                return BadRequest("Hero not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<CityProduct>>> AddCityProduct(CityProduct product)
        {
            city_products.Add(product);
            return Ok(city_products);
        }

        [HttpPut]
        public async Task<ActionResult<List<CityProduct>>> UpdateCityProduct(CityProduct request)
        {
            var product = city_products.Find(p => p.Id == request.Id);
            if (product == null)
                return BadRequest("Hero not found.");

            product.Id = request.Id;
            product.city_id = request.city_id;
            product.product_type = request.product_type;
            product.desired_amount = request.desired_amount;
            product.actual_amount = request.actual_amount;
            product.sell_price = request.sell_price;
            product.buy_price = request.buy_price;
            product.max_fluctation = request.max_fluctation;
            product.min_fluctation = request.min_fluctation;

            return Ok(city_products);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CityProduct>>> Delete(int id)
        {
            var product = city_products.Find(p => p.Id == id);
            if (product == null)
                return BadRequest("Hero not found.");
            city_products.Remove(product);
            return Ok(product);
        }
    }
}
