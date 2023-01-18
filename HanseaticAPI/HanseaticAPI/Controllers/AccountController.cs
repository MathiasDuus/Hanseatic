using HanseaticAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HanseaticAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Account>>> Get(int id)
        {
            var product = await _context.Accounts.FindAsync(id);
            if (product == null)
                return BadRequest("Ship not found.");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> Add(AccountDTO AccountDTO)
        {
            var account = _mapper.Map<Account>(AccountDTO);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Account>>> Update(Account request)
        {
            var account = await _context.Accounts.FindAsync(request.Id);
            if (account == null)
                return BadRequest("Ship not found.");

            account.Username = request.Username;
            account.Password = request.Password;

            await _context.SaveChangesAsync();
            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Account>>> Delete(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return BadRequest("Ship not found.");
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return Ok(await _context.Accounts.ToListAsync());
        }
    }
}
