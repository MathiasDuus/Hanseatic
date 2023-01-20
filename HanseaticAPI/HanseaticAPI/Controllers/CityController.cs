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
            // Return all accounts
            return Ok(await _context.Cities.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<City>>> Get(int id)
        {
            // Check if city exists
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
                return BadRequest("City not found.");

            // Return city
            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<List<City>>> Add(CityDTO cityDTO)
        {
            // Map cityDTO to city
            var city = _mapper.Map<City>(cityDTO);

            // Add city to cities
            _context.Cities.Add(city);

            // Save changes
            await _context.SaveChangesAsync();

            // Return city
            return Ok(city);
        }

        [HttpPut]
        public async Task<ActionResult<List<City>>> Update(City request)
        {
            // Check if city exists
            var city = await _context.Cities.FindAsync(request.Id);
            if (city == null)
                return BadRequest("City not found.");

            // Apply request changes to city
            city.Name = request.Name;

            // Save changes
            await _context.SaveChangesAsync();

            // Return city
            return Ok(city);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<City>>> Delete(int id)
        {
            // Check if city exists
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
                return BadRequest("City not found.");

            // Remove city from cities
            _context.Cities.Remove(city);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all cities
            return Ok(await _context.Cities.ToListAsync());
        }


        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<City>>> GetCityByName(string name)
        {
            // Check if city exists
            var city = await _context.Cities.Where(c => c.Name == name).FirstAsync();
            if (city == null)
                return BadRequest("City not found.");

            // Return City
            return Ok(city);
        }
    }
}
