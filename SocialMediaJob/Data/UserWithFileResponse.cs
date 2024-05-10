using SocialMediaJob.Models;
namespace SocialMediaJob.Data
{
    public class UserWithFileResponse
    {
        public Users Users { get; set; }
        public IFormFile avt { get; set; }
        public IFormFile cv { get; set; }
    }
}
