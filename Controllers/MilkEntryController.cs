using Microsoft.AspNetCore.Mvc;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Controllers;

public class MilkEntryController : BaseController
{
    private readonly IMilkEntryService _milkEntryService;

    public MilkEntryController(IMilkEntryService milkEntryService)
    {
        _milkEntryService = milkEntryService;
    }

    [HttpPost]
    public async Task<ActionResult<MilkEntryResponseDto>> SaveAsync([FromBody] SaveMilkEntryRequestDto requestDto)
    {
        var result = await _milkEntryService.SaveAsync(requestDto);
        if (!result.IsSuccess)
        {
            if (result.ErrorMessage != null && result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(result.ErrorMessage);
            }

            return BadRequest(result.ErrorMessage);
        }

        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data!.MilkEntryId }, result.Data);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MilkEntryResponseDto>>> GetAllAsync()
    {
        var result = await _milkEntryService.GetAllAsync();
        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MilkEntryResponseDto>> GetByIdAsync(int id)
    {
        var result = await _milkEntryService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return Ok(result.Data);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _milkEntryService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return NoContent();
    }
}
