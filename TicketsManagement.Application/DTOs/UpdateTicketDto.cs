using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManagement.Domain.Enums;

namespace TicketsManagement.Application.DTOs
{
    public class UpdateTicketDto
    {
        public string User { get; set; }
        public TicketStatus Status { get; set; }
    }
}
