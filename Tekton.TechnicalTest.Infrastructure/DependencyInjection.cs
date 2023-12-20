using Audit.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tekton.TechnicalTest.Infrastructure.Persistence.Contexts;
using Tekton.TechnicalTest.Infrastructure.Repositories;
using Tekton.TechnicalTest.Infrastructure.Services;
using Tekton.TechnicalTest.Shared.Abstractions;

namespace Tekton.TechnicalTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                if (configuration.GetValue<bool>("UseInMemory"))
                {
                    //options.UseInMemoryDatabase(nameof(AppDbContext));
                }
                else
                {
                    //options.UseSqlServer(configuration.GetConnectionString("Default"));
                    options.UseNpgsql(connectionString);
                }
            });

            Configuration.Setup().UseFileLogProvider(config => config
               .DirectoryBuilder(_ => configuration.GetValue<string>("LoggingPath") + $@"\{DateTime.Now:yyyy-MM-dd}")
               .FilenameBuilder(auditEvent => $"{auditEvent.Environment.UserName}_{DateTime.Now.Ticks}.json"));

            services.AddLazyCache();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();


            services.AddScoped<IExternalService, ExternalService>();

            services.AddScoped(http => new HttpClient());

            return services;
        }

    };
}
