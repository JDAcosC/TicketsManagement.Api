using System.ComponentModel.DataAnnotations;

namespace TicketsManagement.Application.DTOs
{
    public class CreateTicketDto
    {
        [Required]
        public string User { get; set; }
    }
}
