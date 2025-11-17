using System.Linq.Dynamic.Core;
using TicketsManagement.Application.DTOs;
using TicketsManagement.Application.Interfaces;
using TicketsManagement.Domain.Entities;
using TicketsManagement.Domain.Enums;
using TicketsManagement.Domain.Repositories;
using TicketsManagement.Application.Mappings;

namespace TicketsManagement.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<PaginatedList<TicketDto>> GetTicketsAsync(int page, int pageSize)
        {
            var result = await _ticketRepository.GetPagedAsync(page, pageSize);

            var ticketDtos = result.Items.Select(t => t.ToDto());

            return new PaginatedList<TicketDto>
            {
                Items = ticketDtos,
                TotalCount = result.TotalCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<TicketDto?> GetTicketByIdAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            return ticket?.ToDto(); // Devuelve null si no existe o el DTO mapeado
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto ticketDto)
        {
            var now = DateTime.UtcNow;
            var ticket = new Ticket
            {
                User = ticketDto.User,
                Status = TicketStatus.Open, // Lógica de negocio: nace abierto
                CreatedAt = now,
                UpdatedAt = now
            };

            await _ticketRepository.AddAsync(ticket);
            return ticket.ToDto();
        }

        public async Task UpdateTicketAsync(Guid id, UpdateTicketDto ticketDto)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null) throw new KeyNotFoundException($"Ticket {id} no encontrado.");

            // Actualizamos campos
            ticket.User = ticketDto.User;
            ticket.Status = ticketDto.Status;
            ticket.UpdatedAt = DateTime.UtcNow; // Actualizamos fecha

            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task DeleteTicketAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket != null)
            {
                await _ticketRepository.DeleteAsync(ticket);
            }
        }
    }
}
