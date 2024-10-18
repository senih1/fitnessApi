using fitnessApp.Model;
using Microsoft.EntityFrameworkCore;

namespace fitnessApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options){}
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>()
                .HasMany(t => t.Classes)
                .WithOne(c => c.Trainer)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
