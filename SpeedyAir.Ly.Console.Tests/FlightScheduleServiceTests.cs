
using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Tests;

public class FlightScheduleServiceTests
{
    private readonly FlightScheduleService _service;
    private readonly Mock<IFlightScheduleLoader> _flightScheduleLoader;

    public FlightScheduleServiceTests()
    {
        _flightScheduleLoader = new Mock<IFlightScheduleLoader>();
        _service = new FlightScheduleService(_flightScheduleLoader.Object);
    }

    [Fact]
    public async Task ShouldLoadFlightSchedule()
    {
        var expectedFlightSchedule = new FlightSchedule();
        _flightScheduleLoader.Setup(l => l.GetFlightSchedule()).Returns(Task.FromResult(expectedFlightSchedule));
        
        var flightSchedule = await _service.LoadFlightSchedule();

        flightSchedule.Should().Be(expectedFlightSchedule);
    }

    [Fact]
    public async Task ShouldListFlightSchedule()
    {
        _flightScheduleLoader
            .Setup(l => l.GetFlightSchedule()).Returns(Task.FromResult(
                new FlightSchedule
                {
                    Flights = new []
                    {
                        new Flight
                        {
                            Number = 10,
                            Day = 1,
                            Arrival = new Airport{Code = "ARR",Name = "Arrival"},
                            Departure = new Airport{Code = "DEP", Name = "Departure"},
                            Plane = new Plane()
                        }
                    }
                }
                ));
        
        var flightScheduleListed = await _service.ListFlightSchedule();

        flightScheduleListed.First().Should().Be($"Flight: 10, departure: DEP, arrival: ARR, day: 1");
    }
    
}