using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Tests;

public class FlightItinerariesServiceTests
{
    private readonly FlightItinerariesService _service;
    private readonly Mock<IOrdersService> _ordersService;
    private readonly Mock<IFlightScheduleService> _flightScheduleService;

    public FlightItinerariesServiceTests()
    {
        _ordersService = new Mock<IOrdersService>();
        _flightScheduleService = new Mock<IFlightScheduleService>();
        _service = new FlightItinerariesService(_ordersService.Object, _flightScheduleService.Object);
    }

    [Fact]
    public async Task ShouldGenerateFlightItineraries()
    {
        _ordersService
            .Setup(s => s.GetOrders())
            .Returns(Task.FromResult(new Order[]
            {
                new Order{Name = "order-001",Destination = "DEST"},
            }.AsEnumerable()));
        _flightScheduleService
            .Setup(s => s.LoadFlightSchedule())
            .Returns(Task.FromResult(new FlightSchedule
            {
                Flights = new[]
                {
                    new Flight
                    {
                        Number = 1, Day = 1, 
                        Departure = new Airport { Code = "DEP" },
                        Arrival = new Airport { Code = "DEST" }, 
                        Plane = new Plane { Capacity = 1 }
                    }
                }
            }));
        
        var itineraries = await _service.GenerateFlightItineraries();

        itineraries.Single().Should().Be($"order: order-001, flightNumber: 1, departure: DEP, arrival: DEST, day: 1");
    }

    [Fact]
    public void ShouldNotGenerateItinerariesForNoFlightsScheduled()
    {
        var itineraries = _service.GenerateFlightItineraries(new FlightSchedule(), new Order[] { });

        itineraries.Should().BeEmpty();
    }

    [Fact]
    public void ShouldNotScheduleMoreThanCapacity()
    {
        var flight = new Flight
        {
            Number = 1, Day = 1, 
            Departure = new Airport { Code = "DEP1" },
            Arrival = new Airport { Code = "ARR1" }, Plane = new Plane { Capacity = 1 }
        };
        var order1 = new Order { Name = "001", Destination = "ARR1" };
        var order2 = new Order{Name = "002", Destination = "ARR1"};
        
        var flightSchedule = new FlightSchedule
        {
            Flights = new[]
            {
                flight
            }
        };

        var orders = new Order[] { order1, order2 };
        
        var itineraries = _service.GenerateFlightItineraries(flightSchedule, orders);

        itineraries.Where(i => i.Flight != null).Should().HaveCount(1);
        itineraries.Where(i => i.Flight == null).Should().HaveCount(1);
    }
    
}