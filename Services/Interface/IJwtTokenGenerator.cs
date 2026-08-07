using MyFarmAPI.Models.DTOs;
using MyFarmAPI.Models.Entity;

namespace MyFarmAPI.Services.Interface;

public interface IJwtTokenGenerator
{
    AuthResponseDto GenerateToken(User user);
}
