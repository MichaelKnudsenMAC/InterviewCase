using EventContracts.Models;
using MachineSimulator;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace EventContracts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration);
                });

            host.ConfigureServices(services =>
            {
                services.AddSingleton<SimulatorService>();
                services.AddSingleton<MachineInterface>();
                services.AddHostedService(serviceProvider => serviceProvider.GetService<MachineInterface>());
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<ChangeStateConsumer>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ConfigureEndpoints(context);
                    });

                    
                });
            });
            
            return host;
        }

       

    }
}


