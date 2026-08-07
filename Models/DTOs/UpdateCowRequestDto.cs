using System.ComponentModel.DataAnnotations;

namespace MyFarmAPI.Models.DTOs;

public class UpdateCowRequestDto
{
    [Required]
    public int PId { get; set; }

    [Required(ErrorMessage = "CowName is required.")]
    [StringLength(100, ErrorMessage = "CowName cannot exceed 100 characters.")]
    public string CowName { get; set; } = string.Empty;
}
