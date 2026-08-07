namespace MyFarmAPI.Models.DTOs;

public class MilkEntryResponseDto
{
    public int MilkEntryId { get; set; }
    public decimal MilkQuantity { get; set; }
    public int FId { get; set; }
    public string CowName { get; set; } = string.Empty;
    public string ShiftType { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
}
