using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public interface IFlightScheduleService
{   
    Task<FlightSchedule> LoadFlightSchedule();
    Task<IEnumerable<string>> ListFlightSchedule();
}