using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsManagement.Application.DTOs;
using TicketsManagement.Domain.Entities;

namespace TicketsManagement.Application.Mappings
{
    public static class TicketMappingExtensions
    {
        public static TicketDto ToDto(this Ticket ticket)
        {
            return new TicketDto
            {
                Id = ticket.Id,
                User = ticket.User,
                Status = ticket.Status.ToString(), 
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt
            };
        }
    }
}
