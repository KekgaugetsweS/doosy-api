using Doosy.Domain.Constants;
using Doosy.Domain.Entities;
using Doosy.Domain.Requests.Filters;
using Doosy.Framework.Domain;
using Doosy.Framework.Infrastructure;
using Doosy.Infrastructure.DatabaseContexts;
using Doosy.Infrastructure.Repositories;
using Doosy.Infrastructure.Repositories.QueryRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doosy.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<APIDataContext>(options => {
                string connectionString = configuration.GetConnectionString(ConnectionStrings.API);
                options.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString));
            });
            services.AddScoped<DbContext, APIDataContext>();
            services.AddCommandRepositories();
            services.AddQueryRepositories();
            services.AddUtils();


            return services;
        }

         static IServiceCollection AddCommandRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICommandRepository<Person>, CommandRepository<Person>>();
            
            return services;
        }

        static IServiceCollection AddQueryRepositories(this IServiceCollection services)
        {
            services.AddTransient<IQueryRepository<Person,PersonFilter>, PersonQueryRepository>();
            return services;
        }

        static IServiceCollection AddUtils(this IServiceCollection services)
        {
            services.AddTransient<IExcelExporter, ExcelExporter>();
            services.AddTransient<IRestClientBase, RestClientBase>();
            services.AddTransient<ISerializer, Serializer>();

            return services;
        }
    }
}
