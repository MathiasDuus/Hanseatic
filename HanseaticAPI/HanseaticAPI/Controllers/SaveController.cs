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
            // Return all saves
            return Ok(await _context.Saves.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Save>>> Get(int id)
        {
            // Check if save exists
            var save = await _context.Saves.FindAsync(id);
            if (save == null)
                return BadRequest("Save not found.");

            // Return save
            return Ok(save);
        }

        [HttpPost]
        public async Task<ActionResult<List<Save>>> Add(SaveDTO saveDTO)
        {
            // Check if account exists
            var account = await _context.Accounts.FindAsync(saveDTO.AccountId);
            if (account == null)
                return BadRequest("Account not found.");

            // Map saveDTO to save
            var save = _mapper.Map<Save>(saveDTO);

            // Add save to saves
            _context.Saves.Add(save);

            // Save changes
            await _context.SaveChangesAsync();

            // Return save
            return Ok(save);
        }

        [HttpPut]
        public async Task<ActionResult<List<Save>>> Update(Save request)
        {
            // Check if save exists
            var save = await _context.Saves.FindAsync(request.Id);
            if (save == null)
                return BadRequest("Save not found.");

            // Check if account exists
            var account = await _context.Accounts.FindAsync(request.AccountId);
            if (account == null)
                return BadRequest("Account not found.");

            // Apply request changes to save
            save.Date = request.Date;
            save.AccountId = request.AccountId;

            // Save Changes
            await _context.SaveChangesAsync();

            // Return save
            return Ok(save);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Save>>> Delete(int id)
        {
            // Check if save exists
            var save = await _context.Saves.FindAsync(id);
            if (save == null)
                return BadRequest("Save not found.");

            // Remove save from saves
            _context.Saves.Remove(save);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all saves
            return Ok(await _context.Saves.ToListAsync());
        }
    }
}
