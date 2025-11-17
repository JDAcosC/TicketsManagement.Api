using FluentAssertions;
using TicketsManagement.Domain.Entities;
using TicketsManagement.Infrastructure.Persistence.Repositories;
using TicketsManagement.Tests.TestUtilities;
using Xunit;

namespace TicketsManagement.Tests.Repositories
{
    public class TicketRepositoryTests
    {
        [Fact]
        public async Task AddAsync_PersistsEntity()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            var ticket = new Ticket { User = "u1" };
            await repo.AddAsync(ticket);
            await context.SaveChangesAsync();
            (await repo.GetByIdAsync(ticket.Id)).Should().NotBeNull();
        }

        [Fact]
        public async Task Update_ModifiesEntity()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            var ticket = new Ticket { User = "u1" };
            await repo.AddAsync(ticket);
            await context.SaveChangesAsync();
            ticket.User = "u2";
            repo.Update(ticket);
            await context.SaveChangesAsync();
            (await repo.GetByIdAsync(ticket.Id))!.User.Should().Be("u2");
        }

        [Fact]
        public async Task Delete_RemovesEntity()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            var t1 = new Ticket { User = "a" };
            var t2 = new Ticket { User = "b" };
            await repo.AddAsync(t1);
            await repo.AddAsync(t2);
            await context.SaveChangesAsync();
            repo.Delete(t1);
            await context.SaveChangesAsync();
            (await repo.GetByIdAsync(t1.Id)).Should().BeNull();
            (await repo.GetByIdAsync(t2.Id)).Should().NotBeNull();
        }

        [Fact]
        public async Task GetPagedAsync_ReturnsCorrectCounts()
        {
            var context = DbContextFactory.Create();
            var repo = new TicketRepository(context);
            for (int i = 0; i < 10; i++)
            {
                await repo.AddAsync(new Ticket { User = $"u{i}" });
            }
            await context.SaveChangesAsync();
            var (items, total) = await repo.GetPagedAsync(2, 3);
            total.Should().Be(10);
            items.Count().Should().Be(3);
        }
    }
}
