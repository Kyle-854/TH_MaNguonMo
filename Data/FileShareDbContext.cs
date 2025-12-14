using Microsoft.EntityFrameworkCore;

namespace FileShareApp.Data
{
    public class FileShareDbContext : DbContext
    {
        public FileShareDbContext()
        {
        }

        public FileShareDbContext(DbContextOptions<FileShareDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
        }
    }
}