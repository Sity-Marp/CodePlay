using System.Collections.Generic;
namespace Backend.DTOs
{
    public class SubmitResult
    {
        public int AttemptId { get; set; }                 // Id för sparad QuizAttempt (för "visa detaljer")
        public int Correct { get; set; }                   // Antal rätt
        public int Incorrect { get; set; }                 // Antal fel
        public bool Passed { get; set; }                   // Klarade PassingScore?
        public int? NextLevelUnlocked { get; set; }        // Om godkänd: vilken nivå låstes upp
        public List<int> IncorrectQuestionIds { get; set; } = new(); // För att kunna visa vilka som blev fel


        public string? QuizTitle { get; set; }             // T.ex. "HTML – Level 2"
        public int LevelNumber { get; set; } //hej
    }
}
