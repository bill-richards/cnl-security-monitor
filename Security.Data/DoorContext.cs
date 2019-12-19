using Microsoft.EntityFrameworkCore;
using Security.Models;

namespace Security.Data
{
    public class DoorContext : DbContext
    {
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorEvent> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=security.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}