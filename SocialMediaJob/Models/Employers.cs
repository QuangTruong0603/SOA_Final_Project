using System.ComponentModel.DataAnnotations;

namespace SocialMediaJob.Models
{
    public class Employers
    {
        [Key]
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Inudustry { get; set; }
        public string? HeadQuater { get; set; }
        public string? Website { get; set; }
        public string? Decription { get; set; }
        public string? Logo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Job> jobs { get; set; }
        public ICollection<Following> following { get; set; }
    }
}
