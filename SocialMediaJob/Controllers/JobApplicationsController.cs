using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
    public class JobApplicationsController : ControllerBase
    {
        private readonly SocialMediaJobContext _context;

        public JobApplicationsController(SocialMediaJobContext context)
        {
            _context = context;
        }

        // GET: api/JobApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplications>>> GetJobApplications()
        {
          if (_context.JobApplications == null)
          {
              return NotFound();
          }
            return await _context.JobApplications.ToListAsync();
        }

        // GET: api/JobApplications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplications>> GetJobApplications(int id)
        {
          if (_context.JobApplications == null)
          {
              return NotFound();
          }
            var jobApplications = await _context.JobApplications.FindAsync(id);

            if (jobApplications == null)
            {
                return NotFound();
            }

            return jobApplications;
        }

        // PUT: api/JobApplications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobApplications(int id, JobApplications jobApplications)
        {
            if (id != jobApplications.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobApplications).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobApplicationsExists(id))
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

        // POST: api/JobApplications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobApplications>> PostJobApplications(JobApplications jobApplications)
        {
          if (_context.JobApplications == null)
          {
              return Problem("Entity set 'SocialMediaJobContext.JobApplications'  is null.");
          }
            _context.JobApplications.Add(jobApplications);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobApplications", new { id = jobApplications.Id }, jobApplications);
        }

        // DELETE: api/JobApplications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobApplications(int id)
        {
            if (_context.JobApplications == null)
            {
                return NotFound();
            }
            var jobApplications = await _context.JobApplications.FindAsync(id);
            if (jobApplications == null)
            {
                return NotFound();
            }

            _context.JobApplications.Remove(jobApplications);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddJobAppli")]
        public IActionResult AddAply([FromForm] Data.AddJobAplli Apply)
        {
            var user = _context.Users.FirstOrDefault(o => o.Email == Apply.email);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "CV");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Apply.CVFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    if (fileStream != null)
                    {
                        Apply.CVFile.CopyTo(fileStream);
                    }

                }
                var newAplly = new JobApplications
                {
                    UserId = user.UserID,
                    JobId = Apply.JobId,
                    CVPath = uniqueFileName,
                    Timestamp = DateTime.Now,
                    Status = "Aplly",
                };
                _context.JobApplications.Add(newAplly);
                _context.SaveChanges();
                return Ok();
            }
        }

        private bool JobApplicationsExists(int id)
        {
            return (_context.JobApplications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
