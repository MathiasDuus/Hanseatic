using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/ship")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShipController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ship>>> Get()
        {
            return Ok(await _context.Ships.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Ship>>> Get(int id)
        {
            var product = await _context.Ships.FindAsync(id);
            if (product == null)
                return BadRequest("Ship not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<Ship>>> AddCityProduct(ShipDTO ShipDTO)
        {
            var ship = _mapper.Map<Ship>(ShipDTO);
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();
            return Ok(await _context.Ships.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Ship>>> UpdateCityProduct(Ship request)
        {
            var Ship = await _context.Ships.FindAsync(request.Id);
            if (Ship == null)
                return BadRequest("Ship not found.");

            Ship.Name = request.Name;
            Ship.Coin = request.Coin;
            Ship.Save = request.Save;

            await _context.SaveChangesAsync();
            return Ok(await _context.Ships.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Ship>>> Delete(int id)
        {
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null)
                return BadRequest("Ship not found.");
            _context.Ships.Remove(ship);
            await _context.SaveChangesAsync();
            return Ok(await _context.Ships.ToListAsync());
        }
    }
}
