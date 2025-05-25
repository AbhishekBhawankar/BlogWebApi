using BlogWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MstPost> MstPost { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore these properties if they already exist in DB and you don't want EF to manage them
            modelBuilder.Entity<ApplicationUser>().Ignore(x => x.FullName);
            modelBuilder.Entity<ApplicationUser>().Ignore(x => x.MobileNo);
        }
    }
}
