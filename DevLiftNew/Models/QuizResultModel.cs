namespace DevLiftNew.Models;

public class QuizResultModel
{
    public string UserId { get; set; }
    public int Punkte { get; set; }
    public int MaxPunkte { get; set; }
    public int Prozent { get; set; }
    public List<QuizResultQuestionModel> Fragen { get; set; }
}