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

        public DbSet<Quiz> Quizzes { get; set; }             
        public DbSet<Question> Questions { get; set; }        
        public DbSet<AnswerOption> AnswerOptions { get; set; }

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

                // Unikt anv�ndarnamn och email
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

                // Unik per anv�ndare + sp�r
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
                entity.Property(e => e.AnswerOptionId).IsRequired();


                entity.Property(e => e.AnsweredAt)
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(e => e.User)   
                      .WithMany()
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.UserId, e.QuestionId });
            });

            // ===== Quiz =====
            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(q => q.Id);

                entity.Property(q => q.Title)
                      .IsRequired()
                      .HasMaxLength(250); 

                entity.Property(q => q.Track)
                      .IsRequired();

                entity.Property(q => q.LevelNumber)
                      .IsRequired();

                entity.Property(q => q.PassingScore)
                      .HasDefaultValue(0);

                // (Val en quiz per Track+Level)  undviker dubletter
                entity.HasIndex(q => new { q.Track, q.LevelNumber }).IsUnique();
                
            });

            // ===== Question =====
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.Id);

                entity.Property(q => q.Text)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(q => q.Order)
                      .IsRequired()
                      .HasDefaultValue(1); // starta p� 1

                entity.HasOne(q => q.Quiz)
                      .WithMany(z => z.Questions)
                      .HasForeignKey(q => q.QuizId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Unik ordning per quiz
                entity.HasIndex(q => new { q.QuizId, q.Order }).IsUnique();
            });

            // ===== AnswerOption =====
            modelBuilder.Entity<AnswerOption>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Text)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(o => o.IsCorrect)
                      .IsRequired();

                entity.HasOne(o => o.Question)
                      .WithMany(q => q.AnswerOptions)
                      .HasForeignKey(o => o.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(o => o.QuestionId);
                
            });
            //seed data from DataSeeder.cs
            DataSeeder.SeedData(modelBuilder);
        }
    }
}
