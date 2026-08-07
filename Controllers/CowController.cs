using Microsoft.AspNetCore.Mvc;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Controllers;

public class CowController : BaseController
{
    private readonly ICowService _cowService;

    public CowController(ICowService cowService)
    {
        _cowService = cowService;
    }

    [HttpPost]
    public async Task<ActionResult<CowResponseDto>> CreateAsync([FromBody] CreateCowRequestDto requestDto)
    {
        var result = await _cowService.CreateAsync(requestDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data!.PId }, result.Data);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CowResponseDto>>> GetAllAsync()
    {
        var result = await _cowService.GetAllAsync();
        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CowResponseDto>> GetByIdAsync(int id)
    {
        var result = await _cowService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return Ok(result.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CowResponseDto>> UpdateAsync(int id, [FromBody] UpdateCowRequestDto requestDto)
    {
        var result = await _cowService.UpdateAsync(id, requestDto);
        if (!result.IsSuccess)
        {
            if (result.ErrorMessage != null && result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(result.ErrorMessage);
            }

            return BadRequest(result.ErrorMessage);
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _cowService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return NoContent();
    }
}

