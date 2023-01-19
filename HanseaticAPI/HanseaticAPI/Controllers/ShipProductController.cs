using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/ship_product")]
    [ApiController]
    public class ShipProductController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShipProductController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShipProduct>>> Get()
        {
            return Ok(await _context.ShipProducts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ShipProduct>>> Get(int id)
        {
            var product = await _context.ShipProducts.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<ShipProduct>>> Add(ShipProductDTO productDTO)
        {
            var product = _mapper.Map<ShipProduct>(productDTO);
            _context.ShipProducts.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.ShipProducts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ShipProduct>>> Update(ShipProduct request)
        {
            var product = await _context.ShipProducts.FindAsync(request.Id);
            if (product == null)
                return BadRequest("Product not found.");

            product.ShipId = request.ShipId;
            product.ProductTypeId = request.ProductTypeId;
            product.Amount = request.Amount;

            await _context.SaveChangesAsync();
            return Ok(await _context.ShipProducts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ShipProduct>>> Delete(int id)
        {
            var product = await _context.ShipProducts.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found.");
            _context.ShipProducts.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.ShipProducts.ToListAsync());
        }

        [HttpGet("GetProductsByShipId/{id}")]
        public async Task<ActionResult<List<CityProduct>>> GetShipProducsByShipId(int id)
        {
            return Ok(await _context.ShipProducts.Where(c => c.ShipId == id).ToListAsync());
        }
    }
}
