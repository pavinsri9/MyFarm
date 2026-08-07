using MyFarmAPI.Models.Common;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Models.Entity;
using MyFarmAPI.Repositories.Interface;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Services.Implementation;

public class CowService : ICowService
{
    private readonly ICowRepository _cowRepository;

    public CowService(ICowRepository cowRepository)
    {
        _cowRepository = cowRepository;
    }

    public async Task<ServiceResult<CowResponseDto>> CreateAsync(CreateCowRequestDto requestDto)
    {
        if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.CowName))
        {
            return ServiceResult<CowResponseDto>.Failure("CowName is required.");
        }

        var trimmedName = requestDto.CowName.Trim();

        var existingCow = await _cowRepository.GetByNameAsync(trimmedName);
        if (existingCow != null)
        {
            return ServiceResult<CowResponseDto>.Failure("Cow with this name already exists.");
        }

        var cow = new Cow
        {
            CowName = trimmedName
        };

        var createdCow = await _cowRepository.CreateAsync(cow);
        return ServiceResult<CowResponseDto>.Success(MapToDto(createdCow));
    }

    public async Task<ServiceResult<IEnumerable<CowResponseDto>>> GetAllAsync()
    {
        var cows = await _cowRepository.GetAllAsync();
        var dtos = cows.Select(MapToDto);
        return ServiceResult<IEnumerable<CowResponseDto>>.Success(dtos);
    }

    public async Task<ServiceResult<CowResponseDto>> GetByIdAsync(int pId)
    {
        var cow = await _cowRepository.GetByIdAsync(pId);
        if (cow == null)
        {
            return ServiceResult<CowResponseDto>.Failure($"Cow with ID {pId} not found.");
        }

        return ServiceResult<CowResponseDto>.Success(MapToDto(cow));
    }

    public async Task<ServiceResult<CowResponseDto>> UpdateAsync(int pId, UpdateCowRequestDto requestDto)
    {
        if (requestDto == null || string.IsNullOrWhiteSpace(requestDto.CowName))
        {
            return ServiceResult<CowResponseDto>.Failure("CowName is required.");
        }

        var existingCow = await _cowRepository.GetByIdAsync(pId);
        if (existingCow == null)
        {
            return ServiceResult<CowResponseDto>.Failure($"Cow with ID {pId} not found.");
        }

        var trimmedName = requestDto.CowName.Trim();

        var duplicateCow = await _cowRepository.GetByNameAsync(trimmedName);
        if (duplicateCow != null && duplicateCow.PId != pId)
        {
            return ServiceResult<CowResponseDto>.Failure("Another Cow with this name already exists.");
        }

        existingCow.CowName = trimmedName;

        var updatedCow = await _cowRepository.UpdateAsync(existingCow);
        if (updatedCow == null)
        {
            return ServiceResult<CowResponseDto>.Failure($"Cow with ID {pId} not found.");
        }

        return ServiceResult<CowResponseDto>.Success(MapToDto(updatedCow));
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int pId)
    {
        var existingCow = await _cowRepository.GetByIdAsync(pId);
        if (existingCow == null)
        {
            return ServiceResult<bool>.Failure($"Cow with ID {pId} not found.");
        }

        var deleted = await _cowRepository.DeleteAsync(pId);
        if (!deleted)
        {
            return ServiceResult<bool>.Failure($"Failed to delete Cow with ID {pId}.");
        }

        return ServiceResult<bool>.Success(true);
    }

    private static CowResponseDto MapToDto(Cow cow)
    {
        return new CowResponseDto
        {
            PId = cow.PId,
            CowName = cow.CowName
        };
    }
}
