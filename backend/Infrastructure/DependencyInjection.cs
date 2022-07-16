using Application;
using Application.Interfaces;
using Application.Services;
using Infrastructure.BackgroundJobs;
using Infrastructure.Data;
using Infrastructure.Handlers;
using Infrastructure.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration from AppSettings
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            //User Manager Service
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Adding DB Context with MSSQL
            services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IJobSchedulerService, JobSchedulerService>();
            services.AddSignalR();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
