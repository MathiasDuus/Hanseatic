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
            // Return all ships
            return Ok(await _context.Ships.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Ship>>> Get(int id)
        {
            // Check if ship exists
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null)
                return BadRequest("Ship not found.");

            // Return ship
            return Ok(ship);
        }

        [HttpPost]
        public async Task<ActionResult<List<Ship>>> Add(ShipDTO shipDTO)
        {
            // Check if save exists
            var save = await _context.Saves.FindAsync(shipDTO.SaveId);
            if (save == null)
                return BadRequest("Save not found.");

            // Map shipDTO to ship
            var ship = _mapper.Map<Ship>(shipDTO);

            // Add ship to ships
            _context.Ships.Add(ship);

            // Save changes
            await _context.SaveChangesAsync();

            // Return ship
            return Ok(ship);
        }

        [HttpPut]
        public async Task<ActionResult<List<Ship>>> Update(Ship request)
        {
            // Check if save exists
            var save = await _context.Saves.FindAsync(request.SaveId);
            if (save == null)
                return BadRequest("Save not found.");

            // Check if ship exists
            var ship = await _context.Ships.FindAsync(request.Id);
            if (ship == null)
                return BadRequest("Ship not found.");

            // Apply request changes to ship
            ship.Name = request.Name;
            ship.Coin = request.Coin;
            ship.Save = request.Save;

            // Save ships
            await _context.SaveChangesAsync();

            // Return ship
            return Ok(ship);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Ship>>> Delete(int id)
        {
            // Check if ship exists
            var ship = await _context.Ships.FindAsync(id);
            if (ship == null)
                return BadRequest("Ship not found.");

            // Remove ship from ships
            _context.Ships.Remove(ship);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all ships
            return Ok(await _context.Ships.ToListAsync());
        }
    }
}
