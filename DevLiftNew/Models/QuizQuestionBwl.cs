namespace DevLiftNew.Models;

public class QuizQuestionBwl
{
    
    public int Id { get; set; }
    public string BwlKategorie { get; set; }
    public string BwlFrageText { get; set; }
    public List<QuizAnswerBwl>? BwlAnswers { get; set; }
    
}