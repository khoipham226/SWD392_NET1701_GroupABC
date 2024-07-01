using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Model;

namespace SWDProject_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannedAccountsController : ControllerBase
    {
        private readonly SWD392_DBContext _context;

        public BannedAccountsController(SWD392_DBContext context)
        {
            _context = context;
        }

        // GET: api/BannedAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BannedAccount>>> GetBannedAccounts()
        {
          if (_context.BannedAccounts == null)
          {
              return NotFound();
          }
            return await _context.BannedAccounts.ToListAsync();
        }

        // GET: api/BannedAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BannedAccount>> GetBannedAccount(int id)
        {
          if (_context.BannedAccounts == null)
          {
              return NotFound();
          }
            var bannedAccount = await _context.BannedAccounts.FindAsync(id);

            if (bannedAccount == null)
            {
                return NotFound();
            }

            return bannedAccount;
        }

        // PUT: api/BannedAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBannedAccount(int id, BannedAccount bannedAccount)
        {
            if (id != bannedAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(bannedAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannedAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BannedAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BannedAccount>> PostBannedAccount(BannedAccount bannedAccount)
        {
          if (_context.BannedAccounts == null)
          {
              return Problem("Entity set 'SWD392_DBContext.BannedAccounts'  is null.");
          }
            _context.BannedAccounts.Add(bannedAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBannedAccount", new { id = bannedAccount.Id }, bannedAccount);
        }

        // DELETE: api/BannedAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBannedAccount(int id)
        {
            if (_context.BannedAccounts == null)
            {
                return NotFound();
            }
            var bannedAccount = await _context.BannedAccounts.FindAsync(id);
            if (bannedAccount == null)
            {
                return NotFound();
            }

            _context.BannedAccounts.Remove(bannedAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BannedAccountExists(int id)
        {
            return (_context.BannedAccounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
