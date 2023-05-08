using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public class FlightScheduleService : IFlightScheduleService
{
    private readonly IFlightScheduleLoader _flightScheduleLoader;

    public FlightScheduleService(IFlightScheduleLoader flightScheduleLoader)
    {
        _flightScheduleLoader = flightScheduleLoader;
    }
    public async Task<FlightSchedule> LoadFlightSchedule()
    {
        return await _flightScheduleLoader.GetFlightSchedule();
    }

    public async Task<IEnumerable<string>> ListFlightSchedule()
    {
        return (await LoadFlightSchedule()).Flights.Select(f =>
            $"Flight: {f.Number}, departure: {f.Departure.Code}, arrival: {f.Arrival.Code}, day: {f.Day}");
    }
}