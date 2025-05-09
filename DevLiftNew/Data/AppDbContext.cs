using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevLiftNew.Models;

namespace DevLiftNew.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        
        // Deine DbSets für die Tabellen
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }

        // OnModelCreating wird hier zum Konfigurieren von Entitäten verwendet
        protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder); // Muss beibehalten werden, um die Standardmodelle von Identity zu integrieren
    
    // Konfiguration für Flashcards und die Beziehung zu AppUser
    builder.Entity<Flashcard>()
        .HasOne(f => f.CreatedBy) // Flashcard ist mit einem AppUser verbunden
        .WithMany() // Ein AppUser kann viele Flashcards haben
        .HasForeignKey(f => f.CreatedById) // Der Fremdschlüssel ist CreatedById
        .OnDelete(DeleteBehavior.Restrict); // Löschen von Flashcards verhindert, wenn der Benutzer gelöscht wird

    // Konfiguration für QuizQuestion und QuizAnswer
    builder.Entity<QuizQuestion>()
        .HasMany(q => q.Answers) // Eine QuizQuestion kann viele QuizAnswers haben
        .WithOne(a => a.QuizQuestion) // Eine QuizAnswer gehört zu einer QuizQuestion
        .HasForeignKey(a => a.QuizQuestionId) // Der Fremdschlüssel in QuizAnswer ist QuizQuestionId
        .OnDelete(DeleteBehavior.Cascade); // Löschen der Frage löscht auch die Antworten

    // Optional: Du kannst auch für andere Entitäten zusätzliche Beziehungen konfigurieren
}

    }
}