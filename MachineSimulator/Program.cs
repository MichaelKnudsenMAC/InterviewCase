using Serilog;

namespace MachineSimulator
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
            });

            return host;
        }
        
    }
}