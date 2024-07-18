using CustomAuth3.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomAuth3.Database
{
    public class ApplicationDbContext : DbContext
    {
        //this constructor accepts argument for DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.User)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // Optionally, seed roles (admin, user) into the Roles table
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "User" }
            );
        }
    }
}
