
using SupportHub.Api.Models;

namespace SupportHub.Api.DTOs;

public class TicketResponseDto
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TicketStatus Status { get; set; }

    public TicketPriority Priority { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long OpenedById { get; set; }

    public long? AssignedToId { get; set; }
}
