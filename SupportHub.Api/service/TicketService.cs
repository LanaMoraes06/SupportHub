
using Microsoft.EntityFrameworkCore;
using SupportHub.Api.Data;

namespace SupportHub.Api.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    // Criar um chamado
    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
        var user = await _context.Users
            .FindAsync(ticket.OpenedBy.Id);

        if (user is null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        ticket.OpenedBy = user;
        ticket.AssignedTo = null;
        ticket.Comments = new List<Comment>();
        ticket.CreatedAt = DateTime.UtcNow;

        _context.Tickets.Add(ticket);

        await _context.SaveChangesAsync();

        return ticket;
    }

    // Buscar chamado pelo ID
    public async Task<Ticket> GetTicketByIdAsync(long ticketId)
    {
        var ticket = await _context.Tickets
            .Include(t => t.OpenedBy)
            .Include(t => t.AssignedTo)
            .Include(t => t.Comments)
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket is null)
            throw new KeyNotFoundException("Chamado não encontrado.");

        return ticket;
    }

    // Buscar todos os chamados
    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets
            .AsNoTracking()
            .Include(t => t.OpenedBy)
            .Include(t => t.AssignedTo)
            .ToListAsync();
    }

    // Buscar chamados abertos por um usuário
    public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(long userId)
    {
        return await _context.Tickets
            .AsNoTracking()
            .Where(t => t.OpenedBy.Id == userId)
            .Include(t => t.OpenedBy)
            .Include(t => t.AssignedTo)
            .ToListAsync();
    }

    // Atualizar status
    public async Task<Ticket> UpdateTicketStatusAsync(
        long ticketId,
        TicketStatus status)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);

        if (ticket is null)
            throw new KeyNotFoundException("Chamado não encontrado.");

        ticket.Status = status;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return ticket;
    }

    // Atualizar prioridade
    public async Task<Ticket> UpdateTicketPriorityAsync(
        long ticketId,
        TicketPriority priority)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);

        if (ticket is null)
            throw new KeyNotFoundException("Chamado não encontrado.");

        ticket.Priority = priority;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return ticket;
    }

    // Adicionar comentário a um chamado
    public async Task<Comment> AddCommentToTicketAsync(
        long ticketId,
        Comment comment)
    {
        var ticket = await _context.Tickets.FindAsync(ticketId);

        if (ticket is null)
            throw new KeyNotFoundException("Chamado não encontrado.");

        var author = await _context.Users.FindAsync(comment.Author.Id);

        if (author is null)
            throw new KeyNotFoundException("Autor não encontrado.");

        comment.Ticket = ticket;
        comment.Author = author;
        comment.CreatedAt = DateTime.UtcNow;

        _context.Comments.Add(comment);

        ticket.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return comment;
    }
}
