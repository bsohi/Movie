//using BLL.Services;
using BLL.Services;
using Common.Authentication;
using Common.Services;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddInternalRepoServices(configuration);
            return services;
        }
    }
}
