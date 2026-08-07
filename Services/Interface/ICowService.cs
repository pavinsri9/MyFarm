using MyFarmAPI.Models.Common;
using MyFarmAPI.Models.DTOs;

namespace MyFarmAPI.Services.Interface;

public interface ICowService
{
    Task<ServiceResult<CowResponseDto>> CreateAsync(CreateCowRequestDto requestDto);
    Task<ServiceResult<IEnumerable<CowResponseDto>>> GetAllAsync();
    Task<ServiceResult<CowResponseDto>> GetByIdAsync(int pId);
    Task<ServiceResult<CowResponseDto>> UpdateAsync(int pId, UpdateCowRequestDto requestDto);
    Task<ServiceResult<bool>> DeleteAsync(int pId);
}
