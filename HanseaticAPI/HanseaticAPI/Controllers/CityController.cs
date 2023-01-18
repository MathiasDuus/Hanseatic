using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CityController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> Get()
        {
            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<City>>> Get(int id)
        {
            var product = await _context.Cities.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<City>>> AddCityProduct(CityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<City>>> UpdateCityProduct(City request)
        {
            var product = await _context.Cities.FindAsync(request.Id);
            if (product == null)
                return BadRequest("Product not found.");

            product.Name = request.Name;

            await _context.SaveChangesAsync();
            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<City>>> Delete(int id)
        {
            var product = await _context.Cities.FindAsync(id);
            if (product == null)
                return BadRequest("Product not found.");
            _context.Cities.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Cities.ToListAsync());
        }
    }
}
