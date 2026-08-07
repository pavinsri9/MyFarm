using MyFarmAPI.Models.DTOs;

namespace MyFarmAPI.Services.Interface;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserDto createUserDto, string? currentUserName);
    Task<UserDto?> GetByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateUserDto, string? currentUserName);
    Task<bool> SoftDeleteAsync(int id, string? updatedBy);
}