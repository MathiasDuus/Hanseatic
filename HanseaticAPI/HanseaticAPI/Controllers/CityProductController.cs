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
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CityProduct>>> Get(int id)
        {
            var product = await _context.CityProducts.FindAsync(id);
            if (product == null)
                return BadRequest("City_Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<CityProduct>>> Add(CityProductDTO productDTO)
        {
            var product = _mapper.Map<CityProduct>(productDTO);
            _context.CityProducts.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<CityProduct>>> Update(CityProduct request)
        {
            var product = await _context.CityProducts.FindAsync(request.Id);
            if (product == null)
                return BadRequest("City_Product not found.");

            product.CityId = request.CityId;
            product.ProductId = request.ProductId;
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
                return BadRequest("City_Product not found.");
            _context.CityProducts.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpGet("GetByCityId/{id}")]
        public async Task<ActionResult<List<CityProduct>>> GetProductByCityId(int id)
        {
            var product = _context.CityProducts.Where(c => c.CityId == id);

            if (product == null)
                return BadRequest("City_Product not found.");
            return Ok(product);
        }
    }
}
