using MyFarmAPI.Models.Common;
using MyFarmAPI.Models.DTOs;

namespace MyFarmAPI.Services.Interface;

public interface IMilkEntryService
{
    Task<ServiceResult<MilkEntryResponseDto>> SaveAsync(SaveMilkEntryRequestDto requestDto);
    Task<ServiceResult<IEnumerable<MilkEntryResponseDto>>> GetAllAsync();
    Task<ServiceResult<MilkEntryResponseDto>> GetByIdAsync(int milkEntryId);
    Task<ServiceResult<bool>> DeleteAsync(int milkEntryId);
}
