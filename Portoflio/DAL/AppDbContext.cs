using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portoflio.Models;

namespace Portoflio.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<SkillSettings> SkillSettings { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Expeirence> Expeirences { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
