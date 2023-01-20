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
            // Return all accounts
            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Account>>> Get(int id)
        {
            // Check if account exists
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return BadRequest("Account not found.");

            // Return account
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> Add(AccountDTO AccountDTO)
        {
            // Map account dto to account
            var account = _mapper.Map<Account>(AccountDTO);

            // Add acount to accounts
            _context.Accounts.Add(account);

            // Save changes
            await _context.SaveChangesAsync();

            // Return account
            return Ok(account);
        }

        [HttpPut]
        public async Task<ActionResult<List<Account>>> Update(Account request)
        {
            // Check if account exists
            var account = await _context.Accounts.FindAsync(request.Id);
            if (account == null)
                return BadRequest("Account not found.");

            // Apply request changes to account
            account.Username = request.Username;
            account.Password = request.Password;

            // Save changes
            await _context.SaveChangesAsync();

            // Return account
            return Ok(account);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Account>>> Delete(int id)
        {
            // Check if account exists
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return BadRequest("Account not found.");

            // Remove account from accounts
            _context.Accounts.Remove(account);

            // Save changes
            await _context.SaveChangesAsync();

            // Return all accounts
            return Ok(await _context.Accounts.ToListAsync());
        }
    }
}
