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

        /// <summary>
        /// Gets all saves
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Save>>> Get()
        {
            // Return all saves
            return Ok(await _context.Saves.ToListAsync());
        }

        /// <summary>
        /// Gets a single save by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Save>>> Get(int id)
        {
            // Check if save exists
            Save? save = await _context.Saves.FindAsync(id);
            if (save == null)
                return BadRequest("Save not found.");

            // Return save
            return Ok(save);
        }

        /// <summary>
        /// Creates a new save
        /// </summary>
        /// <param name="saveDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<Save>>> Add(SaveDTO saveDTO)
        {
            // Check if account exists
            Account? account = await _context.Accounts.FindAsync(saveDTO.AccountId);
            if (account == null)
                return BadRequest("Account not found.");

            // Map saveDTO to save
            Save? save = _mapper.Map<Save>(saveDTO);

            // Add save to saves
            _context.Saves.Add(save);

            // Save changes
            await _context.SaveChangesAsync();

            // Return save
            return Ok(save);
        }

        /// <summary>
        /// Updates an exsisting save
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<List<Save>>> Update(Save request)
        {
            // Check if save exists
            Save? save = await _context.Saves.FindAsync(request.Id);
            if (save == null)
                return BadRequest("Save not found.");

            // Check if account exists
            Account? account = await _context.Accounts.FindAsync(request.AccountId);
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

        /// <summary>
        /// Deletes a save
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Save>>> Delete(int id)
        {
            // Check if save exists
            Save? save = await _context.Saves.FindAsync(id);
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
