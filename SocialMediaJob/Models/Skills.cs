using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace SocialMediaJob.Models
{
    public class Skills
    {
        [Key]
        public int Id { get; set; }
        public string SkillTitle { get; set; }
        public string? Level { get; set; }
        public string? SkillDescription { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [JsonIgnore]
        public Users Users { get; set; }
    }
}
