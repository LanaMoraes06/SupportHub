public interface ITicketService
{
    Task<Ticket> CreateTicketAsync(Ticket ticket);
    Task<Ticket> GetTicketByIdAsync(long ticketId);
    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(long userId);
    Task<Ticket> UpdateTicketStatusAsync(long ticketId, TicketStatus status);
    Task<Ticket> UpdateTicketPriorityAsync(long ticketId, TicketPriority priority);
    Task<Comment> AddCommentToTicketAsync(long ticketId, Comment comment);
}