using MyFarmAPI.Models.Entity;

namespace MyFarmAPI.Repositories.Interface;

public interface ICowRepository
{
    Task<Cow> CreateAsync(Cow cow);
    Task<IEnumerable<Cow>> GetAllAsync();
    Task<Cow?> GetByIdAsync(int pId);
    Task<Cow?> GetByNameAsync(string cowName);
    Task<Cow?> UpdateAsync(Cow cow);
    Task<bool> DeleteAsync(int pId);
}
