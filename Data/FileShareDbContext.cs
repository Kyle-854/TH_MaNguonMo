using FileShareApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FileShareApp.Data
{
    public partial class FileShareDbContext : DbContext
    {
        public FileShareDbContext()
        {
        }

        public FileShareDbContext(DbContextOptions<FileShareDbContext> options) : base(options)
        {
        }

        public virtual DbSet<SharedFile> Files { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<SharedFile>(entity =>
            {
                entity.HasKey(e => e.FileId).HasName("PRIMARY");

                entity.Property(e => e.UploadDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.User).WithMany(p => p.Files).HasConstraintName("FK_Files_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PRIMARY");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}