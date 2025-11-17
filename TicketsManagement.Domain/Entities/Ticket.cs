using TicketsManagement.Domain.Enums;

namespace TicketsManagement.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string User { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketStatus Status { get; set; }
    }
}
