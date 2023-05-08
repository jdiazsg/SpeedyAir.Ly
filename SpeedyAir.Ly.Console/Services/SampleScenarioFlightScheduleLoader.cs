using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public class SampleScenarioFlightScheduleLoader:IFlightScheduleLoader
{
    public Task<FlightSchedule> GetFlightSchedule()
    {
        var montreal = new Airport{ Code = "YUL", Name = "Montreal" };
        var toronto = new Airport { Code = "YYZ", Name = "Toronto" };
        var calgary = new Airport { Code = "YYC", Name = "Calgary" };
        var vancouver = new Airport { Code = "YVR", Name = "Vancouver" };
        var plane = new Plane{Capacity = 20};
        
        return Task.FromResult(
            new FlightSchedule
            {
                Flights = new[]
                {
                    new Flight
                    {
                        Number = 1, 
                        Day = 1, 
                        Departure = montreal,
                        Arrival = toronto,
                        Plane = plane
                    },
                    new Flight
                    {
                        Number = 2, 
                        Day = 1, 
                        Departure = montreal,
                        Arrival = calgary,
                        Plane = plane
                    },new Flight
                    {
                        Number = 3, 
                        Day = 1, 
                        Departure = montreal,
                        Arrival = vancouver,
                        Plane = plane
                    },
                    
                    new Flight
                    {
                        Number = 4, 
                        Day = 2, 
                        Departure = montreal,
                        Arrival = toronto,
                        Plane = plane
                    },
                    new Flight
                    {
                        Number = 5, 
                        Day = 2, 
                        Departure = montreal,
                        Arrival = calgary,
                        Plane = plane
                    },
                    new Flight
                    {
                        Number = 6, 
                        Day = 2, 
                        Departure = montreal,
                        Arrival = vancouver,
                        Plane = plane
                    },
                }
            }
        );
    }
}