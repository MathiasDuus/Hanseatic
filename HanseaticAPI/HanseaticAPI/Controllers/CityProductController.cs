using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/city_product")]
    [ApiController]
    public class CityProductController : ControllerBase
    {
        private readonly DataContext _context;

        public CityProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityProduct>>> Get()
        {
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CityProduct>>> Get(int id)
        {
            var product = await _context.CityProducts.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<CityProduct>>> AddCityProduct(CityProduct product)
        {
            _context.CityProducts.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<CityProduct>>> UpdateCityProduct(CityProduct request)
        {
            var product = await _context.CityProducts.FindAsync(request.Id);
            if (product == null)
                return BadRequest("Product not found.");

            product.City = request.City;
            product.Product = request.Product;
            product.DesiredAmount = request.DesiredAmount;
            product.ActualAmount = request.ActualAmount;
            product.BasePrice = request.BasePrice;
            product.MaxAmountFluctation = request.MaxAmountFluctation;
            product.MinAmountFluctation = request.MinAmountFluctation;
            product.Save = request.Save;

            await _context.SaveChangesAsync();
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CityProduct>>> Delete(int id)
        {
            var product = await _context.CityProducts.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found.");
            _context.CityProducts.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.CityProducts.ToListAsync());
        }
    }
}
