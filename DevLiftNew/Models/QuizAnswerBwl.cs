namespace DevLiftNew.Models;

public class QuizAnswerBwl
{
    public int Id { get; set; }
    public string BwlAntwortText { get; set; }
    public bool BwlIstKorrekt { get; set; }
    public int bwlQuizQuestionId { get; set; }
    public QuizQuestionBwl QuizQuestionBwl { get; set; }
}