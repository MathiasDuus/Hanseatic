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
            ShipProduct? shipProduct = await _context.ShipProducts.FindAsync(id);
            if (shipProduct == null)
                return BadRequest("Ship Product not found.");

            // Return ship product
            return Ok(shipProduct);
        }

        [HttpPost]
        public async Task<ActionResult<List<ShipProduct>>> Add(ShipProductDTO productDTO)
        {
            // Check if ship exists
            Ship? ship = await _context.Ships.FindAsync(productDTO.ShipId);
            if (ship == null)
                return BadRequest("Ship not found.");

            // Map ship product DTO to ship product
            ShipProduct shipProduct = _mapper.Map<ShipProduct>(productDTO);

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
            ShipProduct? shipProduct = await _context.ShipProducts.FindAsync(request.Id);
            if (shipProduct == null)
                return BadRequest("Product not found.");

            // Check if ship exists
            Ship? ship = await _context.Ships.FindAsync(request.ShipId);
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
            ShipProduct? shipProduct = await _context.ShipProducts.FindAsync(id);
            if (shipProduct == null)
                return BadRequest("Product not found.");

            // Remove ship product from ship products
            _context.ShipProducts.Remove(shipProduct);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all ship products
            return Ok(await _context.ShipProducts.ToListAsync());
        }

        /// <summary>
        /// Adds all products to a ship
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpPost("ship_product_list")]
        public async Task<ActionResult<List<ShipProduct>>> AddAllShipProducts(List<ShipProductDTO> shipProductDTO)
        {
            ShipProduct shipProduct = new();

            // The list used for retirning all the ship_products
            List<ShipProduct> shipProducts = new();
            foreach (ShipProductDTO item in shipProductDTO)
            {
                // Check if ship exists
                Ship? ship = await _context.Ships.FindAsync(item.ShipId);
                if (ship == null)
                    return BadRequest("Ship not found.");

                // Map ship product DTO to ship product
                shipProduct = _mapper.Map<ShipProduct>(item);

                // Add ship product to ship products
                _context.ShipProducts.Add(shipProduct);

                // Save changes
                await _context.SaveChangesAsync();

                shipProducts.Add(shipProduct);
            }

            // Return ship product
            return Ok(shipProducts);
        }


        /// <summary>
        /// Gets all the products on a ship by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetByShipId/{id}")]
        public async Task<ActionResult<List<ShipProduct>>> GetByShipId(int id)
        {
            // Return all ship products where ship id is input id
            return Ok(await _context.ShipProducts.Where(c => c.ShipId == id).ToListAsync());
        }
    }
}
