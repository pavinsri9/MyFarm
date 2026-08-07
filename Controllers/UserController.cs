using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateAsync([FromBody] CreateUserDto createUserDto)
    {
        if (createUserDto == null || string.IsNullOrWhiteSpace(createUserDto.UserName) || string.IsNullOrWhiteSpace(createUserDto.Password))
        {
            return BadRequest("UserName and Password are required.");
        }

        var result = await _userService.CreateAsync(createUserDto, CurrentUserName);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.UserId }, result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetByIdAsync(int id)
    {
        var result = await _userService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound($"User with ID {id} not found.");
        }

        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
    {
        var result = await _userService.GetAllAsync();
        return Ok(result);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDto>> UpdateAsync(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await _userService.UpdateAsync(id, updateUserDto, CurrentUserName);
        if (result == null)
        {
            return NotFound($"User with ID {id} not found.");
        }

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SoftDeleteAsync(int id)
    {
        var updatedBy = !string.IsNullOrWhiteSpace(CurrentUserName) ? CurrentUserName : "System";
        var success = await _userService.SoftDeleteAsync(id, updatedBy);
        if (!success)
        {
            return NotFound($"User with ID {id} not found.");
        }

        return NoContent();
    }
}
