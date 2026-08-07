using BCrypt.Net;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Models.Entity;
using MyFarmAPI.Repositories.Interface;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto createUserDto, string? currentUserName)
    {
        var user = new User
        {
            UserName = createUserDto.UserName,
            Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            CreatedBy = currentUserName,
            CreatedDate = DateTime.UtcNow,
            IsDelete = false
        };

        var createdUser = await _userRepository.CreateAsync(user);
        return MapToDto(createdUser);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : MapToDto(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(MapToDto);
    }

    public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto updateUserDto, string? currentUserName)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
            return null;

        if (!string.IsNullOrWhiteSpace(updateUserDto.UserName))
        {
            existingUser.UserName = updateUserDto.UserName;
            existingUser.UpdatedBy = currentUserName;
        }

        if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
        {
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
        }

        existingUser.UpdatedDate = DateTime.UtcNow;

        var updatedUser = await _userRepository.UpdateAsync(existingUser);
        return updatedUser == null ? null : MapToDto(updatedUser);
    }

    public async Task<bool> SoftDeleteAsync(int id, string? updatedBy)
    {
        return await _userRepository.SoftDeleteAsync(id, updatedBy);
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
        };
    }
}
