using MyFarmAPI.Models.Common;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Models.Entity;
using MyFarmAPI.Repositories.Interface;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Services.Implementation;

public class MilkEntryService : IMilkEntryService
{
    private readonly IMilkEntryRepository _milkEntryRepository;
    private readonly ICowRepository _cowRepository;

    public MilkEntryService(IMilkEntryRepository milkEntryRepository, ICowRepository cowRepository)
    {
        _milkEntryRepository = milkEntryRepository;
        _cowRepository = cowRepository;
    }

    public async Task<ServiceResult<MilkEntryResponseDto>> SaveAsync(SaveMilkEntryRequestDto requestDto)
    {
        if (requestDto == null)
        {
            return ServiceResult<MilkEntryResponseDto>.Failure("Request body cannot be null.");
        }

        if (requestDto.MilkQuantity <= 0)
        {
            return ServiceResult<MilkEntryResponseDto>.Failure("MilkQuantity must be greater than zero.");
        }

        var cow = await _cowRepository.GetByIdAsync(requestDto.FId);
        if (cow == null)
        {
            return ServiceResult<MilkEntryResponseDto>.Failure($"Cow with ID {requestDto.FId} not found.");
        }

        // Check if a record already exists for this cow, date, and shift.
        // If it exists -> UPDATE it automatically.
        // If it does not exist -> CREATE a new record.
        var existingEntry = await _milkEntryRepository.GetExistingEntryAsync(requestDto.FId, requestDto.Date, requestDto.ShiftType);

        if (existingEntry != null)
        {
            // UPDATE existing entry
            existingEntry.MilkQuantity = requestDto.MilkQuantity;

            var updatedEntry = await _milkEntryRepository.UpdateAsync(existingEntry);
            if (updatedEntry == null)
            {
                return ServiceResult<MilkEntryResponseDto>.Failure("Failed to update milk entry.");
            }

            return ServiceResult<MilkEntryResponseDto>.Success(MapToDto(updatedEntry));
        }
        else
        {
            // CREATE new entry
            var newEntry = new MilkEntry
            {
                MilkQuantity = requestDto.MilkQuantity,
                FId = requestDto.FId,
                ShiftType = requestDto.ShiftType,
                Date = requestDto.Date
            };

            var createdEntry = await _milkEntryRepository.CreateAsync(newEntry);
            return ServiceResult<MilkEntryResponseDto>.Success(MapToDto(createdEntry));
        }
    }

    public async Task<ServiceResult<IEnumerable<MilkEntryResponseDto>>> GetAllAsync()
    {
        var entries = await _milkEntryRepository.GetAllAsync();
        var dtos = entries.Select(MapToDto);
        return ServiceResult<IEnumerable<MilkEntryResponseDto>>.Success(dtos);
    }

    public async Task<ServiceResult<MilkEntryResponseDto>> GetByIdAsync(int milkEntryId)
    {
        var entry = await _milkEntryRepository.GetByIdAsync(milkEntryId);
        if (entry == null)
        {
            return ServiceResult<MilkEntryResponseDto>.Failure($"Milk entry with ID {milkEntryId} not found.");
        }

        return ServiceResult<MilkEntryResponseDto>.Success(MapToDto(entry));
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int milkEntryId)
    {
        var existingEntry = await _milkEntryRepository.GetByIdAsync(milkEntryId);
        if (existingEntry == null)
        {
            return ServiceResult<bool>.Failure($"Milk entry with ID {milkEntryId} not found.");
        }

        var deleted = await _milkEntryRepository.DeleteAsync(milkEntryId);
        if (!deleted)
        {
            return ServiceResult<bool>.Failure($"Failed to delete milk entry with ID {milkEntryId}.");
        }

        return ServiceResult<bool>.Success(true);
    }

    private static MilkEntryResponseDto MapToDto(MilkEntry entry)
    {
        return new MilkEntryResponseDto
        {
            MilkEntryId = entry.MilkEntryId,
            MilkQuantity = entry.MilkQuantity,
            FId = entry.FId,
            CowName = entry.Cow?.CowName ?? string.Empty,
            ShiftType = entry.ShiftType.ToString(),
            Date = entry.Date
        };
    }
}
