using Microsoft.EntityFrameworkCore;
using MyFarmAPI.Data;
using MyFarmAPI.Models.Entity;
using MyFarmAPI.Repositories.Interface;

namespace MyFarmAPI.Repositories.Implementation;

public class CowRepository : ICowRepository
{
    private readonly ApplicationDbContext _context;

    public CowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cow> CreateAsync(Cow cow)
    {
        await _context.Cows.AddAsync(cow);
        await _context.SaveChangesAsync();
        return cow;
    }

    public async Task<IEnumerable<Cow>> GetAllAsync()
    {
        return await _context.Cows.ToListAsync();
    }

    public async Task<Cow?> GetByIdAsync(int pId)
    {
        return await _context.Cows
            .FirstOrDefaultAsync(c => c.PId == pId);
    }

    public async Task<Cow?> GetByNameAsync(string cowName)
    {
        return await _context.Cows
            .FirstOrDefaultAsync(c => c.CowName.ToLower() == cowName.ToLower());
    }

    public async Task<Cow?> UpdateAsync(Cow cow)
    {
        var existing = await _context.Cows
            .FirstOrDefaultAsync(c => c.PId == cow.PId);

        if (existing == null)
            return null;

        existing.CowName = cow.CowName;

        _context.Cows.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int pId)
    {
        var existing = await _context.Cows
            .FirstOrDefaultAsync(c => c.PId == pId);

        if (existing == null)
            return false;

        _context.Cows.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
