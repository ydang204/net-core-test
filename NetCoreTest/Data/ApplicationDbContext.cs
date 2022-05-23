using Microsoft.EntityFrameworkCore;
using NetCoreTest.Entities;

namespace NetCoreTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(User).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}