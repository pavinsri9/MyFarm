namespace MyFarmAPI.Models.Entity.Common;

public abstract class BaseEntity
{
    public bool IsDelete { get; set; } = false;
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
}
