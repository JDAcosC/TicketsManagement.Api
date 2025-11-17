using Microsoft.EntityFrameworkCore;
using TicketsManagement.Infrastructure.Persistence;

namespace TicketsManagement.Tests.TestUtilities
{
    public static class DbContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            return new AppDbContext(options);
        }
    }
}
