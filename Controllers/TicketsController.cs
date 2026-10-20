
using Microsoft.AspNetCore.Mvc;
using SupportHub.Api.DTOs;
using SupportHub.Api.Models;
using SupportHub.Api.Services;

namespace SupportHub.Api.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    public async Task<ActionResult<TicketResponseDto>> Create(
        CreateTicketDto dto)
    {
        var ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            OpenedBy = new User { Id = dto.OpenedById }
        };

        try
        {
            var created = await _ticketService
                .CreateTicketAsync(ticket);

            var response = ToResponseDto(created);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                response
            );
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketResponseDto>>>
        GetAll()
    {
        var tickets = await _ticketService.GetAllTicketsAsync();

        return Ok(tickets.Select(ToResponseDto));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<TicketResponseDto>>
        GetById(long id)
    {
        try
        {
            var ticket = await _ticketService
                .GetTicketByIdAsync(id);

            return Ok(ToResponseDto(ticket));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    private static TicketResponseDto ToResponseDto(Ticket ticket)
    {
        return new TicketResponseDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            Priority = ticket.Priority,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt,
            OpenedById = ticket.OpenedBy.Id,
            AssignedToId = ticket.AssignedTo?.Id
        };
    }
}
