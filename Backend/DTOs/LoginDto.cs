namespace Backend.DTOs
{
	//DTO = Data Transfer Object, en "mellan-objekt" för att skicka data frontend-backend
	// Används när en användare loggar in
	public class LoginDto
	{
		public string Username { get; set; }   
		public string Email { get; set; }      // kan logga in med antingen användarnamn eller email
		public string Password { get; set; }   // klartext-lösen för verifiering
	}
}
