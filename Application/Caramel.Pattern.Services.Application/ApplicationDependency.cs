using Caramel.Pattern.Services.Application.Services;
using Caramel.Pattern.Services.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Caramel.Pattern.Services.Application
{
    public static class ApplicationDependency
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
