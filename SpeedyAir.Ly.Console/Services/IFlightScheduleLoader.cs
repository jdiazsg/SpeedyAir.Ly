using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public interface IFlightScheduleLoader
{
    Task<FlightSchedule> GetFlightSchedule();
}