using FluentAssertions;
using TicketsManagement.Domain.Entities;
using TicketsManagement.Domain.Enums;
using TicketsManagement.Application.Mappings;
using Xunit;

namespace TicketsManagement.Tests.Mappings
{
    public class TicketMappingExtensionsTests
    {
        [Fact]
        public void ToDto_MapsFields()
        {
            var now = DateTime.UtcNow;
            var ticket = new Ticket { User = "x", Status = TicketStatus.Closed, CreatedAt = now.AddMinutes(-5), UpdatedAt = now };
            var dto = ticket.ToDto();
            dto.Id.Should().Be(ticket.Id);
            dto.User.Should().Be("x");
            dto.Status.Should().Be(TicketStatus.Closed.ToString());
            dto.CreatedAt.Should().Be(ticket.CreatedAt);
            dto.UpdatedAt.Should().Be(ticket.UpdatedAt);
        }
    }
}
