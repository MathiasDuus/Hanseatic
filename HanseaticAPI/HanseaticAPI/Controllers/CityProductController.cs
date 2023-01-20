using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/city_product")]
    [ApiController]
    public class CityProductController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CityProductController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityProduct>>> Get()
        {
            // Return all city products
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CityProduct>>> Get(int id)
        {
            // Check if city product exists
            var cityProduct = await _context.CityProducts.FindAsync(id);
            if (cityProduct == null)
                return BadRequest("City Product not found.");

            // Return city product
            return Ok(cityProduct);
        }

        [HttpGet("GetByCityId/{id}")]
        public async Task<ActionResult<List<CityProduct>>> GetProductByCityId(int id)
        {
            // Find city product where city id is input id
            var cityProduct = await _context.CityProducts.Where(c => c.CityId == id).ToListAsync();

            // Check if city product exists 
            if (cityProduct == null)
                return BadRequest("City Product not found.");

            // Return city product
            return Ok(cityProduct);
        }

        [HttpPost]
        public async Task<ActionResult<List<CityProduct>>> Add(CityProductDTO cityProductDTO)
        {
            // Check if city exists
            var city = await _context.Cities.FindAsync(cityProductDTO.CityId);
            if (city == null)
                return BadRequest("City not found.");

            // Check if city exists
            var product = await _context.ProductTypes.FindAsync(cityProductDTO.ProductId);
            if (product == null)
                return BadRequest("Product not found.");

            // Check if save exists
            var save = await _context.Saves.FindAsync(cityProductDTO.SaveId);
            if (save == null)
                return BadRequest("Save not found.");

            // Map city product DTO to city product
            var cityProduct = _mapper.Map<CityProduct>(cityProductDTO);

            // Add city product to city products
            _context.CityProducts.Add(cityProduct);

            // Save changes
            await _context.SaveChangesAsync();

            // Return city product
            return Ok(cityProduct);
        }

        [HttpPut]
        public async Task<ActionResult<List<CityProduct>>> Update(CityProduct request)
        {
            // Check if city product exists
            var cityProduct = await _context.CityProducts.FindAsync(request.Id);
            if (cityProduct == null)
                return BadRequest("City Product not found.");

            // Check if city exists
            var city = await _context.Cities.FindAsync(request.CityId);
            if (city == null)
                return BadRequest("City not found.");

            // Check if city exists
            var product = await _context.ProductTypes.FindAsync(request.ProductId);
            if (product == null)
                return BadRequest("Product not found.");

            // Check if save exists
            var save = await _context.Saves.FindAsync(request.SaveId);
            if (save == null)
                return BadRequest("Save not found.");

            // Apply changes to city product
            cityProduct.CityId = request.CityId;
            cityProduct.ProductId = request.ProductId;
            cityProduct.DesiredAmount = request.DesiredAmount;
            cityProduct.ActualAmount = request.ActualAmount;
            cityProduct.BasePrice = request.BasePrice;
            cityProduct.MaxAmountFluctation = request.MaxAmountFluctation;
            cityProduct.MinAmountFluctation = request.MinAmountFluctation;
            cityProduct.Save = request.Save;

            // Save changes
            await _context.SaveChangesAsync();

            // return city product
            return Ok(cityProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CityProduct>>> Delete(int id)
        {
            // Check if city product exists
            var cityProduct = await _context.CityProducts.FindAsync(id);
            if (cityProduct == null)
                return BadRequest("City Product not found.");

            // Remove city product from city products
            _context.CityProducts.Remove(cityProduct);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all city products
            return Ok(await _context.CityProducts.ToListAsync());
        }

    }
}
