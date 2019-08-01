using AopTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AopTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new[] {
             new User{ Id =1,Name="Crash",Address = "Crash" },
             new User{ Id =2,Name="Sol",Address = "Sol" },
             new User{ Id =3,Name="Test",Address = "Test" }

            });
        }
    }
}
