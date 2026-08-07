using MyFarmAPI.Models.Entity;

namespace MyFarmAPI.Repositories.Interface;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUserNameAsync(string userName);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> UpdateAsync(User user);
    Task<bool> SoftDeleteAsync(int id, string? updatedBy);
}
