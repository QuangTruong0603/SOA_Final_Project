using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMediaJob.Models
{
    public class Following
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int FollowerId { get; set; }
        [ForeignKey("Employers")]
        public int EmployerId { get; set; }
        [JsonIgnore]
        public Users? User { get; set; }
        [JsonIgnore]
        public Employers? Employers { get; set; }

    }
}
