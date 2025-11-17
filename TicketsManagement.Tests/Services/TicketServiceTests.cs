using FluentAssertions;
using TicketsManagement.Application.DTOs;
using TicketsManagement.Application.Services;
using TicketsManagement.Domain.Entities;
using TicketsManagement.Domain.Enums;
using TicketsManagement.Infrastructure.Persistence.Repositories;
using TicketsManagement.Tests.TestUtilities;
using Xunit;

namespace TicketsManagement.Tests.Services
{
    public class TicketServiceTests
    {
        [Fact]
        public async Task CreateTicketAsync_SetsStatusOpen()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            var service = new TicketService(repo);
            var dto = await service.CreateTicketAsync(new CreateTicketDto { User = "user1" });
            dto.Status.Should().Be(TicketStatus.Open.ToString());
            dto.User.Should().Be("user1");
            dto.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task GetTicketByIdAsync_ReturnsNullWhenMissing()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            var service = new TicketService(repo);
            var result = await service.GetTicketByIdAsync(Guid.NewGuid());
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateTicketAsync_UpdatesFieldsAndTimestamp()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            var service = new TicketService(repo);
            var ticket = new Ticket { User = "u1", Status = TicketStatus.Open };
            await repo.AddAsync(ticket);
            await context.SaveChangesAsync();
            var before = ticket.UpdatedAt;
            await service.UpdateTicketAsync(ticket.Id, new UpdateTicketDto { User = "u2", Status = TicketStatus.Closed });
            await context.SaveChangesAsync();
            var updated = await repo.GetByIdAsync(ticket.Id);
            updated!.User.Should().Be("u2");
            updated.Status.Should().Be(TicketStatus.Closed);
            updated.UpdatedAt.Should().BeAfter(before);
        }

        [Fact]
        public async Task GetTicketsAsync_ReturnsPagedResult()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            var service = new TicketService(repo);
            for (int i = 0; i < 7; i++)
            {
                await repo.AddAsync(new Ticket { User = $"u{i}" });
            }
            await context.SaveChangesAsync();
            var page = await service.GetTicketsAsync(2, 3);
            page.TotalCount.Should().Be(7);
            page.Items.Count().Should().Be(3);
            page.PageNumber.Should().Be(2);
            page.PageSize.Should().Be(3);
        }
    }
}
