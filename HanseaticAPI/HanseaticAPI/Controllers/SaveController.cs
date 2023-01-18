using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/save")]
    [ApiController]
    public class SaveController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SaveController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Save>>> Get()
        {
            return Ok(await _context.Saves.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Save>>> Get(int id)
        {
            var product = await _context.Saves.FindAsync(id);
            if (product == null)
                return BadRequest("Ship not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<Save>>> Add(SaveDTO SaveDTO)
        {
            var save = _mapper.Map<Save>(SaveDTO);
            _context.Saves.Add(save);
            await _context.SaveChangesAsync();
            return Ok(await _context.Saves.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Save>>> Update(Save request)
        {
            var Ship = await _context.Saves.FindAsync(request.Id);
            if (Ship == null)
                return BadRequest("Ship not found.");

            Ship.Date = request.Date;
            Ship.AccountId = request.AccountId;

            await _context.SaveChangesAsync();
            return Ok(await _context.Saves.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Save>>> Delete(int id)
        {
            var save = await _context.Saves.FindAsync(id);
            if (save == null)
                return BadRequest("Ship not found.");
            _context.Saves.Remove(save);
            await _context.SaveChangesAsync();
            return Ok(await _context.Saves.ToListAsync());
        }
    }
}
