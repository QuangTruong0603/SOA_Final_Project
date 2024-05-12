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
    public class FollowingsController : ControllerBase
    {
        private readonly SocialMediaJobContext _context;

        public FollowingsController(SocialMediaJobContext context)
        {
            _context = context;
        }

        // GET: api/Followings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Following>>> GetFollowings()
        {
          if (_context.Followings == null)
          {
              return NotFound();
          }
            return await _context.Followings.ToListAsync();
        }

        // GET: api/Followings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Following>> GetFollowing(int id)
        {
          if (_context.Followings == null)
          {
              return NotFound();
          }
            var following = await _context.Followings.FindAsync(id);

            if (following == null)
            {
                return NotFound();
            }

            return following;
        }

        // PUT: api/Followings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowing(int id, Following following)
        {
            if (id != following.Id)
            {
                return BadRequest();
            }

            _context.Entry(following).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingExists(id))
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

        // POST: api/Followings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Following>> PostFollowing(Following following)
        {
          if (_context.Followings == null)
          {
              return Problem("Entity set 'SocialMediaJobContext.Followings'  is null.");
          }
            _context.Followings.Add(following);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowing", new { id = following.Id }, following);
        }
        [HttpPost("AddFollow")]
        public IActionResult AddFollow([FromBody] Data.addfollow fl)
        {
            var user = _context.Users.FirstOrDefault(o => o.Username == fl.username);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                var newfl = new Following
                {
                    EmployerId = fl.EmployerId,
                    FollowerId = user.UserID,
                    
                };
                _context.Followings.Add(newfl);
                _context.SaveChanges();
                return Ok();
            }
        }

        // DELETE: api/Followings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowing(int id)
        {
            if (_context.Followings == null)
            {
                return NotFound();
            }
            var following = await _context.Followings.FindAsync(id);
            if (following == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(following);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowingExists(int id)
        {
            return (_context.Followings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
