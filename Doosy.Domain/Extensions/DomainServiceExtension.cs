using Doosy.Domain.Interfaces;
using Doosy.Domain.Requests;
using Doosy.Domain.Services;
using Doosy.Domain.Validators;
using Doosy.Framework.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Doosy.Domain.Extensions
{
    public static class DomainServiceExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddServices();
            services.AddValidators();

            return services;
        }
        static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPersonService, PersonService>();
            return services;
        }
        static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidatorBase<PersonRequest>, PersonValidator>();

            return services;
        }
    }
}
