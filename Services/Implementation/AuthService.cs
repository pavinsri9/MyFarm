using BCrypt.Net;
using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Repositories.Interface;
using MyFarmAPI.Services.Interface;

namespace MyFarmAPI.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        if (string.IsNullOrWhiteSpace(loginDto.UserName) || string.IsNullOrWhiteSpace(loginDto.Password))
        {
            return null;
        }

        var user = await _userRepository.GetByUserNameAsync(loginDto.UserName);
        if (user == null)
        {
            return null;
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
        if (!isPasswordValid)
        {
            return null;
        }

        return _jwtTokenGenerator.GenerateToken(user);
    }
}
