using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SocialMediaJob.Data;
using SocialMediaJob.Models;

namespace SocialMediaJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SocialMediaJobContext _context;

        public UsersController(SocialMediaJobContext context)
        {
            _context = context;
        }

        // GET: api/Users
        /*  [HttpGet]
          public IActionResult GetUsers(string email)
          {
              var User = _context.Users.Include(x => x.skills)
                  .Include(x => x.educations)
                  .FirstOrDefault(x => x.Email == email);
              if (User == null)
              {
                  return NotFound();
              }
              else
              {
                  return Ok(User);
              }
          }
  */
        // GET: api/Users/5
        [HttpGet("{email}")]
        public async Task<ActionResult<Users>> GetUsers(string email)
        {
            if(_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users
                .Include(x => x.educations).
                Include(x => x.posts).
                Include(x => x.skills).FirstOrDefaultAsync(x => x.Email.Equals(email));


            if (users == null)
            {
                return NotFound();
            }
            else
            {
                return users;
            }
        }

            

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.UserID)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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
        [HttpPost("UloadAvatar")]
        public IActionResult UploadAvt([FromForm] Data.UploadAvt avt)
        {

            var user = _context.Users.FirstOrDefault(o => o.Email == avt.email);
            if (avt != null && user != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + avt.avt.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    if (fileStream != null)
                    {
                        avt.avt.CopyTo(fileStream);
                    }

                }
                user.AvatarPath = uniqueFileName;
                _context.SaveChanges();
                return Ok();


            }
            return BadRequest();
        }

        [HttpPost("UploadCV")]
        public IActionResult UploadCV([FromForm] Data.UploadAvt avt)
        {

            var user = _context.Users.FirstOrDefault(o => o.Email == avt.email);
            if (avt != null && user != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "CV");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + avt.avt.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    if (fileStream != null)
                    {
                        avt.avt.CopyTo(fileStream);
                    }

                }
                user.CVPath = uniqueFileName;
                _context.SaveChanges();
                return Ok();


            }
            return BadRequest();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Avatar")]
        public IActionResult UploadFile(IFormFile formFile)
        {

            var user = _context.Users.FirstOrDefault(o => o.UserID == 1);
            if (formFile != null && user != null)
            {
                /* string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");*/
                /* string uploadsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img");*/
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "img");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    if (fileStream != null)
                    {
                        formFile.CopyTo(fileStream);
                    }

                }
                user.AvatarPath = uniqueFileName;
                _context.SaveChanges();
                return Ok();


            }
            return BadRequest();
            // Trả về view hoặc thực hiện các xử lý khác nếu form không chứa tệp tin

        }
        [HttpGet("GetAvatar")]
        public IActionResult GetFile(string email)
        {

            var user = _context.Users.FirstOrDefault(o => o.Email == email);
            // Đường dẫn tới thư mục chứa ảnh
            string imageDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "img");

            if (user.AvatarPath != null) 
            {
                // Tên của tệp ảnh cố định
                string imageName = user.AvatarPath;

                // Đường dẫn đến tệp ảnh
                string imagePath = Path.Combine(imageDirectoryPath, imageName);
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
            

            // Kiểm tra xem tệp ảnh có tồn tại không
            

        }

        [HttpGet("GetCV")]
        public IActionResult GetCV(string email)
        {

            var user = _context.Users.FirstOrDefault(o => o.Email == email);
            // Đường dẫn tới thư mục chứa ảnh
            string imageDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "CV");

            if (user.CVPath != null)
            {
                // Tên của tệp ảnh cố định
                string imageName = user.CVPath;

                // Đường dẫn đến tệp ảnh
                string imagePath = Path.Combine(imageDirectoryPath, imageName);
                if (System.IO.File.Exists(imagePath))
                {
                    // Trả về tệp ảnh dưới dạng PhysicalFileResult
                    return PhysicalFile(imagePath, "application/pdf");
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


            // Kiểm tra xem tệp ảnh có tồn tại không


        }





        [HttpPost("login")]
        public IActionResult CheckLogin([FromBody] Data.Login loginModel)
        {
            if(loginModel !=  null)
            {
                var user = _context.Users.FirstOrDefault(i => i.Email == loginModel.email && i.Password == loginModel.password);
                if (user != null)
                {
                    return Ok();
                }
            }
            return BadRequest();
            
        }

        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
