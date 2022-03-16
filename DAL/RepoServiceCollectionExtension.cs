using DAL.Models;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace DAL
{
    public static class RepoServiceCollectionExtension
    {
        public static IServiceCollection AddInternalRepoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieSaaSContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MovieSaaS"));
            });

            services.AddScoped<IMovieRepo, MovieRepo>();
            services.AddScoped<IListValuesRepo, ListValuesRepo>();

            return services;
        }
    }
}
