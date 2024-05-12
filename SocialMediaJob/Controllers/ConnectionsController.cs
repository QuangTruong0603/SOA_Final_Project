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
    public class ConnectionsController : ControllerBase
    {
        private readonly SocialMediaJobContext _context;

        public ConnectionsController(SocialMediaJobContext context)
        {
            _context = context;
        }

        // GET: api/Connections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Connection>>> GetConnections()
        {
          if (_context.Connections == null)
          {
              return NotFound();
          }
            return await _context.Connections.ToListAsync();
        }

        // GET: api/Connections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Connection>> GetConnection(int id)
        {
          if (_context.Connections == null)
          {
              return NotFound();
          }
            var connection = await _context.Connections.FindAsync(id);

            if (connection == null)
            {
                return NotFound();
            }

            return connection;
        }

        // PUT: api/Connections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConnection(int id, Connection connection)
        {
            if (id != connection.Id)
            {
                return BadRequest();
            }

            _context.Entry(connection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConnectionExists(id))
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

        // POST: api/Connections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Connection>> PostConnection(Connection connection)
        {
          if (_context.Connections == null)
          {
              return Problem("Entity set 'SocialMediaJobContext.Connections'  is null.");
          }
            _context.Connections.Add(connection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConnection", new { id = connection.Id }, connection);
        }

        // DELETE: api/Connections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnection(int id)
        {
            if (_context.Connections == null)
            {
                return NotFound();
            }
            var connection = await _context.Connections.FindAsync(id);
            if (connection == null)
            {
                return NotFound();
            }

            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("notconnect")]
        public async Task<ActionResult<IEnumerable<Users>>> GetEmployersNotFollow(string username)
        {
            var userids = await _context.Users.FirstOrDefaultAsync(o => o.Username == username);
           
            if (_context.Employers == null)
            {
                return NotFound();
            }
            var employer = await _context.Users.Include(o => o.connections)
                .Where(e => !e.connections.Any())
                .Where(e => e.Username != username)
                .ToListAsync();
          
            return employer;
        }

        [HttpPost("AddFriend")]
        public IActionResult AddFriend([FromBody] Data.Addfriend af)
        {
            var user = _context.Users.FirstOrDefault(o => o.Email == af.requestemail);
            if (af == null)
            {
                return BadRequest();
            }
            else
            {
                var Connection = new Connection()
                {
                    ConnectionType = 1,
                    RequestId = af.requestemail,
                    ReceiverId = af.receiveemail,
                    Confirm = false,
                    userId = user.UserID,
                  
                };
                _context.Connections.Add(Connection);
                _context.SaveChanges();
                return Ok();
            }
        }
        [HttpGet("GetFriendNoAccept")]
        public async Task<ActionResult<IEnumerable<Users>>> GetFriendNoAccept(string email)
        {
            var user = await _context.Users
                .Include(o => o.connections)
                .Where(o => o.connections.Any(o => o.Confirm == false ))
                .Where(o => o.connections.Any(o => o.ReceiverId == email))
                .Where(o => o.connections.Any(o => o.ConnectionType == 1))
                .Where(o => o.Email !=  email)
                .ToListAsync();
               
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                return user;
            }
        }

        private bool ConnectionExists(int id)
        {
            return (_context.Connections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
