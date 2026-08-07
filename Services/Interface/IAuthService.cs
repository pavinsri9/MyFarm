using MyFarmAPI.Models.DTOs;

namespace MyFarmAPI.Services.Interface;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}
