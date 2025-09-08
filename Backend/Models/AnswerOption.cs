namespace Backend.Models
{
    public class AnswerOption
    {
        public int Id { get; set; }                      // Primärnyckel
        public string Text { get; set; } = "";           // Text som visas i UI
        public bool IsCorrect { get; set; }              // TRUE om detta är rätt svar

       
        public int QuestionId { get; set; }              // FK
        public Question? Question { get; set; }
    }
}
