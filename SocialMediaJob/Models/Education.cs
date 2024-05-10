using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace SocialMediaJob.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string?  Major { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [JsonIgnore]
        public Users Users { get; set; }
    }
}
