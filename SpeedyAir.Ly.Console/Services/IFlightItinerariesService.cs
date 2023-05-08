namespace SpeedyAir.Ly.Console.Services;

public interface IFlightItinerariesService
{
    Task<IEnumerable<string>> GenerateFlightItineraries();
}