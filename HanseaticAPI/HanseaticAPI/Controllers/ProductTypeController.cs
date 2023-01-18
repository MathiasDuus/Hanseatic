using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/product_type")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductTypeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductType>>> Get()
        {
            return Ok(await _context.ProductTypes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductType>>> Get(int id)
        {
            var type = await _context.ProductTypes.FindAsync(id);
            if (type == null)
                return BadRequest("Product Type not found.");
            return Ok(type);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductType>>> AddCityProduct(ProductType type)
        {
            _context.ProductTypes.Add(type);
            await _context.SaveChangesAsync();
            return Ok(await _context.ProductTypes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ProductType>>> UpdateCityProduct(ProductType request)
        {
            var type = await _context.ProductTypes.FindAsync(request.Id);
            if (type == null)
                return BadRequest("Product Type not found.");

            type.Name = request.Name;

            await _context.SaveChangesAsync();
            return Ok(await _context.ProductTypes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductType>>> Delete(int id)
        {
            var Type = await _context.ProductTypes.FindAsync(id);
            if (Type == null)
                return BadRequest("Product Type not found.");
            _context.ProductTypes.Remove(Type);
            await _context.SaveChangesAsync();
            return Ok(await _context.ProductTypes.ToListAsync());
        }
    }
}
