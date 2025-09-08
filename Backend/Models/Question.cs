using System.Collections.Generic;
namespace Backend.Models
{
    public class Question
    {
        public int Id { get; set; }                     // Primärnyckel
        public string Text { get; set; } = "";          // Själva frågetexten som visas i UI

        // FK till Quiz 
        public int QuizId { get; set; }                 // FK
        public Quiz? Quiz { get; set; }                 // Navigation till sitt quiz

        
        public int Order { get; set; } = 1;             // Visningsordning

       
        public List<AnswerOption> AnswerOptions { get; set; } = new();
    }
}
