namespace DevLiftNew.Models;

public class QuizResultQuestion
{
    public int Id { get; set; }
    public int QuizResultBwlId { get; set; }  
    public QuizResultBwl QuizResultBwl { get; set; }
    
    public int QuizQuestionBwlId { get; set; }  
    public QuizQuestionBwl QuizQuestionBwl { get; set; }
    
    public bool IsCorrect { get; set; }  
}