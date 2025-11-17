using Microsoft.Extensions.DependencyInjection;
using TicketsManagement.Application.Interfaces;
using TicketsManagement.Application.Services;

namespace TicketsManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();

         

            return services;
        }
    }
}
