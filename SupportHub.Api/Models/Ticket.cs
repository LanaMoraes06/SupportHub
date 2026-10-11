public class Ticket{
    public long Id{get; set;}

    public string Title{get; set;} = string.Empty;

    public string Description{get; set;} = string.Empty;

    public TicketStatus Status{get; set;} = TicketStatus.Open;

    public TicketPriority Priority{get; set;} = TicketPriority.Low;

    public DateTime CreatedAt{get; set;} = DateTime.Now;

    public DateTime? UpdatedAt{get; set;} 

    public User OpenedBy{get; set;} = new User();

    public User? AssignedTo{get; set;} 

    public List<Comment> Comments{get; set;} = new List<Comment>();
}