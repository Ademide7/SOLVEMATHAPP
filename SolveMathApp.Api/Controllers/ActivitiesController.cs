using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolveMathApp.Application.Interfaces;
using SolveMathApp.Domain.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SolveMathApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ActivitiesController(IActivityService activityService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllActivities()
        {
            var result = await activityService.GetAllActivities();
            return Ok(result);
		}

        [HttpPost("calculate-factorial")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CalculateFactorial([FromBody] CalculateFactorialRequest dto)
        {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
			{
				return Unauthorized(new { Status = false, Message = "Invalid user!" });
			}
			Guid userIdGuid = Guid.Parse(userIdClaim.Value);
			var result = await activityService.CalculateFactorial
            (new CalculateFactorialDto(dto.Number,userIdGuid));
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
		}

        [HttpPost("calculate-age")]
        public async Task<IActionResult> CalculateAge([FromBody] CalculateAgeRequest dto)
        {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
			{
				return Unauthorized(new { Status = false, Message = "Invalid user!" });
			}
			Guid userIdGuid = Guid.Parse(userIdClaim.Value);
			var result = await activityService.CalculateAge
			(new CalculateAgeDto(dto.BirthYear, userIdGuid)); 
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
		}

        [HttpPost("calculate-distance")]
        public async Task<IActionResult> CalculateDistance([FromBody] CalculateDistanceRequest dto)
        {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
			{
				return Unauthorized(new { Status = false, Message = "Invalid user!" });
			}
			Guid userIdGuid = Guid.Parse(userIdClaim.Value);
            var result = await activityService.CalculateDistance(new CalculateDistanceDto
            (
                dto.X1,
                dto.Y1,
                dto.X2,
                dto.Y2,
                userIdGuid
            ));
			if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
		}

        [HttpGet("user-activities")]
        public async Task<IActionResult> GetUserActivities(int page, int pageSize)
        {
			// Extract userId from the authenticated user's claims.
            var userIdClaim  = User.FindFirst(ClaimTypes.NameIdentifier);
		 
			if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized(new { Status = false, Message = "Invalid user!" });
			}
            Guid userIdGuid = Guid.Parse(userIdClaim.Value);
			var result = await activityService.GetUserActivities(userIdGuid,page,pageSize);
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
		}

	}
}
