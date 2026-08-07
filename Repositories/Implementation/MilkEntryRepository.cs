using Microsoft.EntityFrameworkCore;
using MyFarmAPI.Data;
using MyFarmAPI.Models.Entity;
using MyFarmAPI.Models.Enums;
using MyFarmAPI.Repositories.Interface;

namespace MyFarmAPI.Repositories.Implementation;

public class MilkEntryRepository : IMilkEntryRepository
{
    private readonly ApplicationDbContext _context;

    public MilkEntryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MilkEntry> CreateAsync(MilkEntry entry)
    {
        await _context.MilkEntries.AddAsync(entry);
        await _context.SaveChangesAsync();

        // Reload entry with Cow navigation property for response mapping
        await _context.Entry(entry).Reference(m => m.Cow).LoadAsync();
        return entry;
    }

    public async Task<IEnumerable<MilkEntry>> GetAllAsync()
    {
        return await _context.MilkEntries
            .Include(m => m.Cow)
            .ToListAsync();
    }

    public async Task<MilkEntry?> GetByIdAsync(int milkEntryId)
    {
        return await _context.MilkEntries
            .Include(m => m.Cow)
            .FirstOrDefaultAsync(m => m.MilkEntryId == milkEntryId);
    }

    public async Task<MilkEntry?> GetExistingEntryAsync(int fId, DateOnly date, ShiftType shiftType)
    {
        return await _context.MilkEntries
            .Include(m => m.Cow)
            .FirstOrDefaultAsync(m => m.FId == fId && m.Date == date && m.ShiftType == shiftType);
    }

    public async Task<MilkEntry?> UpdateAsync(MilkEntry entry)
    {
        var existing = await _context.MilkEntries
            .FirstOrDefaultAsync(m => m.MilkEntryId == entry.MilkEntryId);

        if (existing == null)
            return null;

        existing.MilkQuantity = entry.MilkQuantity;
        existing.FId = entry.FId;
        existing.ShiftType = entry.ShiftType;
        existing.Date = entry.Date;

        _context.MilkEntries.Update(existing);
        await _context.SaveChangesAsync();

        await _context.Entry(existing).Reference(m => m.Cow).LoadAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int milkEntryId)
    {
        var existing = await _context.MilkEntries
            .FirstOrDefaultAsync(m => m.MilkEntryId == milkEntryId);

        if (existing == null)
            return false;

        _context.MilkEntries.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
