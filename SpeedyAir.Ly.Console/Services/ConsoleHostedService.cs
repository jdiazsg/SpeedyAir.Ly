using Microsoft.Extensions.Hosting;

namespace SpeedyAir.Ly.Console.Services;

public class ConsoleHostedService : IHostedService
{
    private readonly Options _options;
    private readonly IFlightScheduleService _flightScheduleService;
    private readonly IFlightItinerariesService _flightItinerariesService;

    public ConsoleHostedService(Options options, IFlightScheduleService flightScheduleService, IFlightItinerariesService flightItinerariesService)
    {
        _flightItinerariesService = flightItinerariesService;
        _options = options;
        _flightScheduleService = flightScheduleService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        switch (_options.Operation)
        {
            case Operation.ListFlightSchedule:
                var flightScheduleListStrings = await _flightScheduleService.ListFlightSchedule();
                System.Console.Write(string.Join(Environment.NewLine, flightScheduleListStrings));
                break;
            case Operation.GenerateFlightItineraries:
                var itineraries = await _flightItinerariesService.GenerateFlightItineraries();
                System.Console.Write(string.Join(Environment.NewLine, itineraries));

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        
    }
}