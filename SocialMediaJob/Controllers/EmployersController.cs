using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaJob.Data;
using SocialMediaJob.Models;

namespace SocialMediaJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly SocialMediaJobContext _context;

        public EmployersController(SocialMediaJobContext context)
        {
            _context = context;
        }

        // GET: api/Employers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employers>>> GetEmployers()
        {
          if (_context.Employers == null)
          {
              return NotFound();
          }
            return await _context.Employers.Include(o => o.jobs).ToListAsync();
        }
        [HttpGet("notfollow")]
        public async Task<ActionResult<IEnumerable<Employers>>> GetEmployersNotFollow(string username)
        {
            var  userids = await _context.Users.FirstOrDefaultAsync(o => o.Username == username);
            if (_context.Employers == null)
            {
                return NotFound();
            }
            var employer = await _context.Employers.Include(o => o.following)
                .Where(e => !e.following.Any(follow => follow.FollowerId == userids.UserID))
                .ToListAsync();
            return employer;
        }
       
        // GET: api/Employers/5
        [HttpGet("{username}")]

        public async Task<ActionResult<IEnumerable<Employers>>> GetEmployersFollow(string username)
        {
            var userids = await _context.Users.FirstOrDefaultAsync(o => o.Username == username);
            if (_context.Employers == null)
            {
                return NotFound();
            }
            var employer = await _context.Employers.Include(o => o.following)
                .Where(e => e.following.Any(follow => follow.FollowerId == userids.UserID))
                .ToListAsync();
            return employer;
        }

        // PUT: api/Employers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployers(int id, Employers employers)
        {
            if (id != employers.Id)
            {
                return BadRequest();
            }

            _context.Entry(employers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployersExists(id))
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

        // POST: api/Employers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employers>> PostEmployers(Employers employers)
        {
          if (_context.Employers == null)
          {
              return Problem("Entity set 'SocialMediaJobContext.Employers'  is null.");
          }
            _context.Employers.Add(employers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployers", new { id = employers.Id }, employers);
        }

        // DELETE: api/Employers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployers(int id)
        {
            if (_context.Employers == null)
            {
                return NotFound();
            }
            var employers = await _context.Employers.FindAsync(id);
            if (employers == null)
            {
                return NotFound();
            }

            _context.Employers.Remove(employers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployersExists(int id)
        {
            return (_context.Employers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
