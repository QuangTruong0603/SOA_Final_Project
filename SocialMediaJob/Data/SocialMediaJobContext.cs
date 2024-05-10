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
    }
}
