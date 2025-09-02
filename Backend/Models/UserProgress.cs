namespace Backend.Models
{
    public class UserProgress
    {
        //Håller koll på en användarens progress i en viss track (tex html)
        public int Id { get; set; } //Primary key
        public int UserId { get; set; } //Foreign key to User
        public TrackType Track { get; set; } //The track the användaren är i


        //Nivåprogress
        public int CurrentLevel { get; set; } //Nuvarande level the användaren är på
        public int HighestUnlockedLevel { get; set; } //Högsta level the användaren har låst upp


        //Poängprogress
        public int TotalPoints { get; set; } //Totala poäng the användaren har tjänat
        public int TotalCorrect { get; set; }
        public int TotalIncorrect { get; set; }
        public DateTime LastUpdated { get; set; }=DateTime.UtcNow; //Senast uppdaterad tidpunkt

        public User User { get; set; } //Navigation property to User


    }
}
