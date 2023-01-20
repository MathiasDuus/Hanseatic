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

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<City>>> Get()
        {
            // Return all accounts
            return Ok(await _context.Cities.ToListAsync());
        }

        /// <summary>
        /// Get a city by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a new city
        /// </summary>
        /// <param name="cityDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update a city
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete a city
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get CityId based on the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
