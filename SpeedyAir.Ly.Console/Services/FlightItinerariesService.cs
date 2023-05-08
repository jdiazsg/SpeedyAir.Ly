using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public class FlightItinerariesService : IFlightItinerariesService
{
    private readonly IOrdersService _ordersService;
    private readonly IFlightScheduleService _flightScheduleService;

    public FlightItinerariesService(IOrdersService ordersService, IFlightScheduleService flightScheduleService)
    {
        _flightScheduleService = flightScheduleService;
        _ordersService = ordersService;
    }
    
    public async Task<IEnumerable<string>> GenerateFlightItineraries()
    {
        var orders = await _ordersService.GetOrders();
        var schedule = await _flightScheduleService.LoadFlightSchedule();

        var itineraries = GenerateFlightItineraries(schedule, orders);

        return itineraries.Select(i =>
            i.Flight != null ? 
                $"order: {i.Order.Name}, flightNumber: {i.Flight.Number}, departure: {i.Flight.Departure.Code}, arrival: {i.Flight.Arrival.Code}, day: {i.Flight.Day}" : 
                $"order: {i.Order.Name}, flightNumber: not scheduled");
    }

    public List<FlightItinerary> GenerateFlightItineraries(FlightSchedule schedule, IEnumerable<Order> orders)
    {
        var ordersByDestination = orders.GroupBy(o => o.Destination,
            (dest, orders) => new { Destination = dest, Orders = orders })
            .ToDictionary(o => o.Destination, arg => new Queue<Order>(arg.Orders));

        var itineraries = new List<FlightItinerary>();
        foreach (var flight in schedule.Flights ?? new Flight[]{})
        {
            var ordersQueue = ordersByDestination[flight.Arrival.Code];

            itineraries.AddRange( DequeueOrders(ordersQueue, flight.Plane.Capacity)
                .Select(o => new FlightItinerary { Flight = flight, Order = o }));
        }

        foreach (var destinationWithUnscheduledOrders in ordersByDestination.Keys.Where(k => ordersByDestination[k].Count > 0))
        {
            var ordersQueue = ordersByDestination[destinationWithUnscheduledOrders];
            var unscheduledOrders = DequeueOrders(ordersQueue, ordersQueue.Count);
            itineraries.AddRange(unscheduledOrders.Select(o => new FlightItinerary{Order = o}));
        }

        return itineraries;
    }

    private IEnumerable<Order> DequeueOrders(Queue<Order> ordersQueue, int planeCapacity)
    {
        for (int i = 0; i < planeCapacity && ordersQueue.Count > 0; i++)
        {
            yield return ordersQueue.Dequeue();
        }
    }
}