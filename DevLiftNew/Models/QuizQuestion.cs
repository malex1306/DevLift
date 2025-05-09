namespace DevLiftNew.Models;


    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Kategorie { get; set; }
        public string FrageText { get; set; }
        public List<QuizAnswer>? Answers { get; set; }
        // public ICollection<QuizAnswer>? Answers { get; set; }
    }
