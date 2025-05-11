namespace DevLiftNew.Models;

public class QuizResultBwl
{
    public int Id { get; set; }
    public string UserId {get; set;}
    public DateTime Date {get; set;}
    public int Punkte {get; set;}
    public int MaxPunkte {get; set;}
    public int Prozent {get; set;}
    public List<QuizResultQuestion> BeantworteteFragen { get; set; } = new();
}