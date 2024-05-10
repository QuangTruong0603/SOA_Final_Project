using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaJob.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        public string? Username { get; set; }

     
        public string? Email { get; set; }

 
        public string? Password { get; set; }

        public string? Fullname { get; set; }

        public string? AvatarPath { get; set; }
        [NotMapped]
        public IFormFile? AvtImage { get; set; }

        public string? CVPath { get; set; }
        [NotMapped]
        public IFormFile? CVFile { get; set; }
        public int? role { get; set; }
        // Địa chỉ và số điện thoại
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Skills> skills { get; set; }
        public ICollection<Education> educations { get; set; }
        public ICollection<Post> posts { get; set; }

    }
}
