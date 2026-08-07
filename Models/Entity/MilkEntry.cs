using MyFarmAPI.Models.Enums;

namespace MyFarmAPI.Models.Entity;

public class MilkEntry
{
    public int MilkEntryId { get; set; }
    public decimal MilkQuantity { get; set; }
    public int FId { get; set; }
    public ShiftType ShiftType { get; set; }
    public DateOnly Date { get; set; }

    // Navigation property
    public Cow? Cow { get; set; }
}
