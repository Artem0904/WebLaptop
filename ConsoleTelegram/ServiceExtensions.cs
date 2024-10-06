using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IBotUserService, BotUserService>();
        }
    }
}
