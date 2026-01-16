using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolveMathApp.Application.Interfaces;
using SolveMathApp.Domain.Dtos;

namespace SolveMathApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("validate")]
        public async Task<IActionResult> ValidateUser([FromForm] string email, [FromForm] string password)
        {
            try
            {
                var result = await userService.ValidateUser(email, password);
                if (!result.Status)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = false, Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromForm] string token, [FromForm] string email)
        {
            var result = await userService.RefreshToken(token, email);
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // create user endpoint
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var result = await userService.CreateUser(userDto);
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        //GET ALL USERS
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers(int page, int pageSize)
        {
            var result = await userService.GetAllUsers(page,pageSize);
            return Ok(result);
        }
    }
}
