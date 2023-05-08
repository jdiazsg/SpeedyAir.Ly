using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpeedyAir.Ly.Console.Services;

namespace SpeedyAir.Ly.Console;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices(services =>
                    {
                        services.AddSingleton(options);
                        
                        services.AddScoped<IFlightScheduleLoader, SampleScenarioFlightScheduleLoader>();
                        services.AddScoped<IFlightScheduleService, FlightScheduleService>();
                        services.AddScoped<IOrdersService, JsonOrdersService>(s => new JsonOrdersService(options.FileName));
                        services.AddScoped<IFlightItinerariesService, FlightItinerariesService>();
                        
                        services.AddHostedService<ConsoleHostedService>();
                    })
                    .Build();
                
                host.Run();
            });
    }
}