using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialMediaJob.Data;
using SocialMediaJob.Models;

namespace SocialMediaJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly SocialMediaJobContext _context;

        public PostsController(SocialMediaJobContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            return await _context.Posts.ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Post>> GetPost(string email)
        {
          if (_context.Posts == null)
          {
              return NotFound();
          }
            var post = await _context.Posts.FirstOrDefaultAsync(o => o.username.Equals(email));

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
          if (_context.Posts == null)
          {
              return Problem("Entity set 'SocialMediaJobContext.Posts'  is null.");
          }
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        [HttpPost("UploadPost")]
        public IActionResult uploadPost([FromForm] Data.Post2 post)
        {
            var User = _context.Users.FirstOrDefault(o => o.Email.Equals(post.email));
            if (post != null && User != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Post");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + post.img.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    if (fileStream != null)
                    {
                        post.img.CopyTo(fileStream);
                    }

                }
                var newpost = new Post
                {
                    Content = post.content,
                    Contentfile = uniqueFileName,
                    username = post.email,
                    UserId = User.UserID,
                    Created = DateTime.Now,
                };
                _context.Posts.Add(newpost);
                _context.SaveChanges();
                return Ok();


            }
            return BadRequest();
        }
        [HttpGet("GetContentFile")]
        public IActionResult GetImgPost(string imgpath)
        {
           
            // Đường dẫn tới thư mục chứa ảnh
            string imageDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Post");

            if (imgpath != null)
            {

                // Đường dẫn đến tệp ảnh
                string imagePath = Path.Combine(imageDirectoryPath, imgpath);
                if (System.IO.File.Exists(imagePath))
                {
                    // Trả về tệp ảnh dưới dạng PhysicalFileResult
                    return PhysicalFile(imagePath, "image/jpeg");
                    //return Ok(imagePath);
                }
                else
                {
                    // Trả về lỗi 404 nếu không tìm thấy tệp ảnh
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosts(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
