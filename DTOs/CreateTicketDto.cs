
using System.ComponentModel.DataAnnotations;

namespace SupportHub.Api.DTOs;

public class CreateTicketDto
{
    [Required]
    [StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000, MinimumLength = 5)]
    public string Description { get; set; } = string.Empty;

    [Range(1, long.MaxValue)]
    public long OpenedById { get; set; }
}
