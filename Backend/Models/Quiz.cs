using System.Collections.Generic;
namespace Backend.Models
{
    public class Quiz
    {
        public int Id { get; set; } //PK
        public string Title { get; set; } //Rubrik som visas i UI
        public TrackType Track { get; set; } //Vilket spår quizen tillhör

        public int LevelNumber { get; set; } //Vilken nivå quizen tillhör

        //Poäng som krävs för att klara quizen
        public int PassingScore { get; set; } = 0;
        public List<Question> Questions { get; set; } = new(); //Lista med frågor som tillhör quizen
    }
}
