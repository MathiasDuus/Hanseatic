using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/product_type")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductTypeController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductType>>> Get()
        {
            // Return all product types
            return Ok(await _context.ProductTypes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductType>>> Get(int id)
        {
            // Check if product type exists
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
                return BadRequest("Product Type not found.");

            // Return product type
            return Ok(productType);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductType>>> Add(ProductTypeDTO typeDTO)
        {
            // Map product type DTO to product type
            ProductType? productType = _mapper.Map<ProductType>(typeDTO);

            // Add product type to product types
            _context.ProductTypes.Add(productType);

            // Save changes
            await _context.SaveChangesAsync();

            // Return product type
            return Ok(productType);
        }

        [HttpPut]
        public async Task<ActionResult<List<ProductType>>> Update(ProductType request)
        {
            // Check if product type exists
            ProductType? productType = await _context.ProductTypes.FindAsync(request.Id);
            if (productType == null)
                return BadRequest("Product Type not found.");

            // Apply request changes to product type
            productType.Name = request.Name;

            // Save changes
            await _context.SaveChangesAsync();

            // Return product type
            return Ok(productType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductType>>> Delete(int id)
        {
            // Check if product type exists
            ProductType? productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
                return BadRequest("Product Type not found.");

            // Remove product type from product types
            _context.ProductTypes.Remove(productType);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all product Types
            return Ok(await _context.ProductTypes.ToListAsync());
        }
    }
}
