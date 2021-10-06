
using Distro2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Distro2.Data
{
    public class UserContext : IdentityDbContext<Distro2User>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }


        public DbSet<Distro2User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
