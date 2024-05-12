using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMediaJob.Models
{
    public class JobApplications
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public string? CVPath { get; set; }  
        public DateTime? Timestamp { get; set; }
        public string? Status { get; set; }
        [JsonIgnore]
        public Job Job { get; set; }
        [JsonIgnore]
        public Users Users { get; set; }
    }
}
