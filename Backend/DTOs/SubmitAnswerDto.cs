namespace Backend.DTOs
{
   
    
       // Det här skickar frontend när användaren skickar in ett quiz
        public class SubmitAnswersDto
        {
            public int QuizId { get; set; } // vilket quiz som besvaras
            public List<AnswerSubmissionDto> Answers { get; set; } = new();
        }

        // En rad per fråga: vilken fråga och vilket alternativ användaren valde
        public class AnswerSubmissionDto
        {
            public int QuestionId { get; set; }
            public int AnswerOptionId { get; set; }// t.ex. "<h1>"
        }
    
}
