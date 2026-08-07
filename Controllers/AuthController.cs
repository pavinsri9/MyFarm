using Microsoft.AspNetCore.Mvc;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> LoginAsync([FromBody] LoginDto loginDto)
    {
        if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.UserName) || string.IsNullOrWhiteSpace(loginDto.Password))
        {
            return BadRequest("UserName and Password are required.");
        }

        var authResult = await _authService.LoginAsync(loginDto);
        if (authResult == null)
        {
            return Unauthorized("Invalid UserName or Password.");
        }

        return Ok(authResult);
    }
}
