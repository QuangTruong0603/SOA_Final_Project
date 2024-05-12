using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SocialMediaJob.Models
{
    public class Job
    {
        [Key]
        public int JObId { get; set; }
        public string? JobTitle { get; set; }
        public string? Location { get; set; }
        public string? EmploymenType { get; set; }
        public string? RequirementSkill { get; set; }
        public string? RequirementLevel { get; set; }
        public string? Depcription { get; set; }
        public Decimal? Salary { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? Timestamp { get; set; }
        [ForeignKey("Employer")]
        public int EmployerId { get; set; }
        [JsonIgnore]
        public Employers Employers { get; set; }
        public ICollection<JobApplications> JobApplications { get; set; }
    }
}
