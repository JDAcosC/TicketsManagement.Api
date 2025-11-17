
using System.Linq.Dynamic.Core;
using TicketsManagement.Application.DTOs;

namespace TicketsManagement.Application.Interfaces
{
    public interface ITicketService
    {
        Task<PaginatedList<TicketDto>> GetTicketsAsync(int page, int pageSize);
        Task<TicketDto?> GetTicketByIdAsync(Guid id);
        Task<TicketDto> CreateTicketAsync(CreateTicketDto ticketDto);
        Task UpdateTicketAsync(Guid id, UpdateTicketDto ticketDto);
        Task DeleteTicketAsync(Guid id);
    }
}
