using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevLiftNew.Models;

namespace DevLiftNew.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<QuizQuestionBwl> QuizQuestionsBwl { get; set; }
        public DbSet<QuizAnswerBwl> QuizAnswersBwl { get; set; }
        public DbSet<QuizResultBwl> QuizResultBwl { get; set; }
        public DbSet<QuizResultQuestion> QuizResultQuestion { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Beziehung Flashcard ↔ AppUser
            builder.Entity<Flashcard>()
                .HasOne(f => f.CreatedBy)
                .WithMany()
                .HasForeignKey(f => f.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Beziehung QuizQuestion ↔ QuizAnswer
            builder.Entity<QuizQuestion>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.QuizQuestion)
                .HasForeignKey(a => a.QuizQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Beziehung QuizQuestionBwl ↔ QuizAnswerBwl
            builder.Entity<QuizAnswerBwl>()
                .HasOne(a => a.QuizQuestionBwl)
                .WithMany(q => q.BwlAnswers)
                .HasForeignKey(a => a.bwlQuizQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<QuizResultQuestion>()
                .HasOne(qr => qr.QuizResultBwl)
                .WithMany(q => q.BeantworteteFragen)
                .HasForeignKey(qr => qr.QuizResultBwlId);

            builder.Entity<QuizResultQuestion>()
                .HasOne(qr => qr.QuizQuestionBwl)
                .WithMany()
                .HasForeignKey(qr => qr.QuizQuestionBwlId);
            
        }
    }
}