public class Comment
{
    public long Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User Author { get; set; } = new User();

    public Ticket Ticket { get; set; } = new Ticket();
}