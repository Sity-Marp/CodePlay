namespace Backend.Models
{
    //Sparar resultat för varje enskild fråga the användaren svarar på
    public class UserAnswer
    {
        public int Id { get; set; } //Primary key
        public int UserId { get; set; } //Foreign key to User
        public int QuestionId { get; set; } //Foreign key to Question
        public bool IsCorrect { get; set; } //Om svaret var korrekt eller inte
        public string SelectedOption { get; set; } //Det valda svaret
        public int AnswerOptionId { get; set; } //Foreign key to AnswerOption
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

        public int QuizAttemptId { get; set; }               // FK till QuizAttempt
        public QuizAttempt QuizAttempt { get; set; }         // navigation

        public User User { get; set; } //Navigation property to User
    }
}
