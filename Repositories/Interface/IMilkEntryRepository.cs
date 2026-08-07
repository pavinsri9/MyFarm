using MyFarmAPI.Models.Entity;
using MyFarmAPI.Models.Enums;

namespace MyFarmAPI.Repositories.Interface;

public interface IMilkEntryRepository
{
    Task<MilkEntry> CreateAsync(MilkEntry entry);
    Task<IEnumerable<MilkEntry>> GetAllAsync();
    Task<MilkEntry?> GetByIdAsync(int milkEntryId);
    Task<MilkEntry?> GetExistingEntryAsync(int fId, DateOnly date, ShiftType shiftType);
    Task<MilkEntry?> UpdateAsync(MilkEntry entry);
    Task<bool> DeleteAsync(int milkEntryId);
}
