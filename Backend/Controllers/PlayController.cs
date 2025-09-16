using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

//Endpoints som tar emot inskickade quiz-svar och sparar i DB
namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlayController : ControllerBase
    {
        private readonly PlayService _play;

        public PlayController(PlayService play)
        {
            _play = play;
        }

        // POST /api/play/submit  (kräver att man är inloggad och skickar JWT)
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] SubmitAnswersDto dto)
        {
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized(new { message = "Ingen giltig user-id i token." });

            var result = await _play.SubmitAsync(userId, dto);
            return Ok(result);
        }

    }
}
