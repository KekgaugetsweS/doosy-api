using Doosy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Doosy.Infrastructure.DatabaseContexts
{
    public class APIDataContext:DbContext
    {
        IConfiguration configuration;

        public APIDataContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var constring = configuration.GetConnectionString("ApiConnection");
            optionsBuilder.
                UseMySql(constring, ServerVersion.AutoDetect(constring));
        }

        public DbSet<Person> Person { get; set; }

    }
}
