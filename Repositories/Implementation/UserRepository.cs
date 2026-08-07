using Microsoft.EntityFrameworkCore;
using MyFarmAPI.Data;
using MyFarmAPI.Models.Entity;
using MyFarmAPI.Repositories.Interface;

namespace MyFarmAPI.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == id && !u.IsDelete);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDelete);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => !u.IsDelete)
            .ToListAsync();
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var existing = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == user.UserId && !u.IsDelete);

        if (existing == null)
            return null;

        existing.UserName = user.UserName;
        if (!string.IsNullOrEmpty(user.Password))
        {
            existing.Password = user.Password;
        }
        existing.UpdatedBy = user.UpdatedBy;
        existing.UpdatedDate = DateTime.UtcNow;

        _context.Users.Update(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> SoftDeleteAsync(int id, string? updatedBy)
    {
        var existing = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == id && !u.IsDelete);

        if (existing == null)
            return false;

        existing.IsDelete = true;
        existing.UpdatedBy = updatedBy;
        existing.UpdatedDate = DateTime.UtcNow;

        _context.Users.Update(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
