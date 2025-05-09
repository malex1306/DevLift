namespace DevLiftNew.Models;

public class QuizAnswer
{
    public int Id { get; set; }
    public string AntwortText { get; set; }
    public bool IstKorrekt { get; set; }
    public int QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; }
}