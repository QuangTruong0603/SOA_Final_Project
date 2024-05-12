using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMediaJob.Models
{
    public class Connection
    {
        [Key]
        public int Id { get; set; }
        public int? ConnectionType { get; set; }
        public string RequestId { get; set; }
        public string ReceiverId { get; set;}
        [ForeignKey("Users")]
        public int userId { get; set; }
        [JsonIgnore]
        public Users? User { get; set; }
        public Boolean Confirm { get; set; } 
    }
}
