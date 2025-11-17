using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TicketsManagement.Domain.Repositories;
using TicketsManagement.Infrastructure.Persistence;
using TicketsManagement.Infrastructure.Persistence.Repositories;

namespace TicketsManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

           
            services.AddScoped<ITicketRepository, TicketRepository>();

            

            return services;
        }
    }
}
