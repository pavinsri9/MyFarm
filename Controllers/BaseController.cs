using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MyFarmAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Gets the current authenticated User ID from JWT claims.
    /// Returns 0 if not authenticated or claim is missing/invalid.
    /// </summary>
    protected int CurrentUserId
    {
        get
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("userId")?.Value;

            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            return 0;
        }
    }

    /// <summary>
    /// Gets the current authenticated User Name from JWT claims.
    /// Returns null or empty string if not authenticated or claim is missing.
    /// </summary>
    protected string CurrentUserName
    {
        get
        {
            return User.FindFirst(ClaimTypes.Name)?.Value
                ?? User.FindFirst("userName")?.Value
                ?? string.Empty;
        }
    }
}
