using HR.Application.Contracts.Email;
using HR.Application.Contracts.Logger;
using HR.Application.Models.Email;
using HR.Lib.AppLogger;
using HR.Lib.EmailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Lib
{
    public static class LibServiceRegistration
    {
        public static IServiceCollection AddLibServices(this IServiceCollection services, IConfiguration config)
        {
            // Email
            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            // Logger
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}
