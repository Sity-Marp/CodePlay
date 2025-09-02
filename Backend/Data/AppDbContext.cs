using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tabeller i databasen
        public DbSet<User> Users { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== User =====
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.PasswordHash).IsRequired();

                // Unikt användarnamn och email
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // ===== UserProgress =====
            modelBuilder.Entity<UserProgress>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Track).IsRequired();
                entity.Property(e => e.CurrentLevel).IsRequired();
                entity.Property(e => e.HighestUnlockedLevel).IsRequired();

                entity.Property(e => e.TotalPoints).IsRequired();
                entity.Property(e => e.TotalCorrect).IsRequired();
                entity.Property(e => e.TotalIncorrect).IsRequired();

                entity.Property(e => e.LastUpdated)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.User)   
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Unik per användare + spår
                entity.HasIndex(e => new { e.UserId, e.Track }).IsUnique();
            });

            // ===== UserAnswer =====
            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.IsCorrect).IsRequired();
                entity.Property(e => e.SelectedOption)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.AnsweredAt)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.User)   
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.UserId, e.QuestionId });
            });
        }
    }
}
