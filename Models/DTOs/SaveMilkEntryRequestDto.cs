using System.ComponentModel.DataAnnotations;
using MyFarmAPI.Models.Enums;

namespace MyFarmAPI.Models.DTOs;

public class SaveMilkEntryRequestDto
{
    [Required(ErrorMessage = "MilkQuantity is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "MilkQuantity must be greater than zero.")]
    public decimal MilkQuantity { get; set; }

    [Required(ErrorMessage = "FId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "FId must be a valid Cow ID.")]
    public int FId { get; set; }

    [Required(ErrorMessage = "ShiftType is required.")]
    public ShiftType ShiftType { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    public DateOnly Date { get; set; }
}
