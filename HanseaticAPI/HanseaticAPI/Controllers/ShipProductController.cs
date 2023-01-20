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
            // Return all ship products
            return Ok(await _context.ShipProducts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ShipProduct>>> Get(int id)
        {
            // Check if ship product exists
            var shipProduct = await _context.ShipProducts.FindAsync(id);
            if (shipProduct == null)
                return BadRequest("Ship Product not found.");

            // Return ship product
            return Ok(shipProduct);
        }

        [HttpPost]
        public async Task<ActionResult<List<ShipProduct>>> Add(ShipProductDTO productDTO)
        {
            // Check if ship exists
            var ship = await _context.Ships.FindAsync(productDTO.ShipId);
            if (ship == null)
                return BadRequest("Ship not found.");

            // Map ship product DTO to ship product
            var shipProduct = _mapper.Map<ShipProduct>(productDTO);

            // Add ship product to ship products
            _context.ShipProducts.Add(shipProduct);

            // Save changes
            await _context.SaveChangesAsync();

            // Return ship product
            return Ok(shipProduct);
        }

        [HttpPut]
        public async Task<ActionResult<List<ShipProduct>>> Update(ShipProduct request)
        {
            // Check if ship product exists
            var shipProduct = await _context.ShipProducts.FindAsync(request.Id);
            if (shipProduct == null)
                return BadRequest("Product not found.");

            // Check if ship exists
            var ship = await _context.Ships.FindAsync(request.ShipId);
            if (ship == null)
                return BadRequest("Ship not found.");

            // Apply request changes to ship product
            shipProduct.ShipId = request.ShipId;
            shipProduct.ProductTypeId = request.ProductTypeId;
            shipProduct.Amount = request.Amount;

            // Save changes
            await _context.SaveChangesAsync();

            // Return ship product
            return Ok(shipProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ShipProduct>>> Delete(int id)
        {
            // Check if ship product exists
            var shipProduct = await _context.ShipProducts.FindAsync(id);
            if (shipProduct == null)
                return BadRequest("Product not found.");

            // Remove ship product from ship products
            _context.ShipProducts.Remove(shipProduct);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all ship products
            return Ok(await _context.ShipProducts.ToListAsync());
        }

        [HttpGet("GetProductsByShipId/{id}")]
        public async Task<ActionResult<List<CityProduct>>> GetShipProducsByShipId(int id)
        {
            // Return all ship products where ship id is input id
            return Ok(await _context.ShipProducts.Where(c => c.ShipId == id).ToListAsync());
        }
    }
}
