using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMediaJob.Models;

namespace SocialMediaJob.Data
{
    public class SocialMediaJobContext : DbContext
    {
        public SocialMediaJobContext (DbContextOptions<SocialMediaJobContext> options)
            : base(options)
        {
        }

        public DbSet<SocialMediaJob.Models.Users> Users { get; set; } = default!;
        public DbSet<SocialMediaJob.Models.Education> Educations { get; set; }= default!;
        public DbSet<SocialMediaJob.Models.Skills> Skills { get; set; } = default!;
        public DbSet<SocialMediaJob.Models.Post> Posts { get; set; } = default!;
        public DbSet<SocialMediaJob.Models.Job> Jobs { get; set; } = default!;
        public DbSet<SocialMediaJob.Models.JobApplications> JobApplications { get; set; } = default!;
        public DbSet<SocialMediaJob.Models.Employers> Employers { get; set; } = default!;
        public DbSet<SocialMediaJob.Models.Connection> Connections { get; set; } = default!;
        public DbSet<SocialMediaJob.Models.Following> Followings { get; set; } = default!;
    }
}
