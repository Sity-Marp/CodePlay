namespace Backend.DTOs
{
	//DTO = Data Transfer Object, en "mellan-objekt" f�r att skicka data frontend-backend
	// Anv�nds n�r en anv�ndare loggar in
	public class LoginDto
	{
		public string Username { get; set; }   
		public string Email { get; set; }      // kan logga in med antingen anv�ndarnamn eller email
		public string Password { get; set; }   // klartext-l�sen f�r verifiering
	}
}
