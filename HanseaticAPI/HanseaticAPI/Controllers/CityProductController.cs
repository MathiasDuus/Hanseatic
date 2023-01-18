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
                return BadRequest("Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<CityProduct>>> AddCityProduct(CityProductDTO productDTO)
        {
            var product = _mapper.Map<CityProduct>(productDTO);
            _context.CityProducts.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.CityProducts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<CityProduct>>> UpdateCityProduct(CityProductDTO requestDTO)
        {
            var product = await _context.CityProducts.FindAsync(requestDTO.Id);
            if (product == null)
                return BadRequest("Product not found.");

            product.CityId = requestDTO.CityId;
            product.Product = requestDTO.Product;
            product.DesiredAmount = requestDTO.DesiredAmount;
            product.ActualAmount = requestDTO.ActualAmount;
            product.BasePrice = requestDTO.BasePrice;
            product.MaxAmountFluctation = requestDTO.MaxAmountFluctation;
            product.MinAmountFluctation = requestDTO.MinAmountFluctation;
            product.Save = requestDTO.Save;

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
