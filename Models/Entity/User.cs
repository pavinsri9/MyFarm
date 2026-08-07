using MyFarmAPI.Models.Entity.Common;

namespace MyFarmAPI.Models.Entity;

public class User : BaseEntity
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
