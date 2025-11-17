using Microsoft.AspNetCore.Mvc;
using TicketsManagement.Application.DTOs;
using TicketsManagement.Application.Interfaces;

namespace TicketsManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;


        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pagedResult = await _ticketService.GetTicketsAsync(page, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound($"Ticket con ID {id} no encontrado.");
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTicket = await _ticketService.CreateTicketAsync(createTicketDto);

            return CreatedAtAction(nameof(GetTicketById), new { id = newTicket.Id }, newTicket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] UpdateTicketDto updateTicketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

   
            await _ticketService.UpdateTicketAsync(id, updateTicketDto);

   
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return NoContent();
        }
    }
}
