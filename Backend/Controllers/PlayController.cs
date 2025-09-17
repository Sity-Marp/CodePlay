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
    public class PlayController : ControllerBase
    {
        private readonly PlayService _play;

        public PlayController(PlayService play)
        {
            _play = play;
        }

        // POST /api/play/submit  (kräver att man är inloggad och skickar JWT)
        [HttpPost("submit")]
        [Authorize]
        public async Task<IActionResult> Submit([FromBody] SubmitAnswersDto dto)
        {
            //  1. Hämta userId från JWT-token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Ingen userId i token.");
            var userId = int.Parse(userIdClaim);

            //  2. Validera quizId
            if (dto == null || dto.QuizId <= 0)
                return BadRequest("QuizId saknas eller är ogiltigt.");

            //  3. Validera att det finns minst ett svar
            if (dto.Answers == null || dto.Answers.Count == 0)
                return BadRequest("Du måste välja minst ett alternativ.");

            //  4. Validera att alla svar har ett alternativ valt
            if (dto == null || dto.QuizId <= 0)
                return BadRequest("QuizId saknas eller är ogiltigt.");

            if (dto.Answers == null || dto.Answers.Count == 0)
                return BadRequest("Du måste välja minst ett alternativ.");

            foreach (var ans in dto.Answers)
            {
                if (ans.QuestionId <= 0 || ans.AnswerOptionId <= 0)
                    return BadRequest("Varje svar måste ha giltig QuestionId och AnswerOptionId.");
            }

            try
            {
                //  5. Lämna över till PlayService som rättar och sparar
                var result = await _play.SubmitAsync(userId, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
