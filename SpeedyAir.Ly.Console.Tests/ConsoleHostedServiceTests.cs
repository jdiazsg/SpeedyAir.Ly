namespace SpeedyAir.Ly.Console.Tests;

public class ConsoleHostedServiceTests
{
    private readonly Mock<IFlightScheduleService> _flightScheduleService = new Mock<IFlightScheduleService>();
    private readonly Mock<IFlightItinerariesService> _flightItinerariesService = new Mock<IFlightItinerariesService>();
    private readonly ConsoleHostedService _consoleHostedService;
    private readonly Options _options = new Options();
    private readonly StringWriter _consoleStringWriter = new StringWriter();

    public ConsoleHostedServiceTests()
    {
        _consoleHostedService = new ConsoleHostedService(_options, _flightScheduleService.Object, _flightItinerariesService.Object);
        
        System.Console.SetOut(_consoleStringWriter);
    }
    
    [Fact]
    public async Task ShouldListFlightSchedule()
    {
        _options.Operation = Operation.ListFlightSchedule;

        _flightScheduleService.Setup(s => s.ListFlightSchedule())
            .Returns(Task.FromResult(new[] { "flight1", "flight2" }.AsEnumerable()));

        await _consoleHostedService.StartAsync();

        _consoleStringWriter.ToString().Should().Be($"flight1{Environment.NewLine}flight2");
    }

    [Fact]
    public async Task ShouldGenerateFlightItineraries()
    {
        _options.Operation = Operation.GenerateFlightItineraries;

        _flightItinerariesService.Setup(s => s.GenerateFlightItineraries())
            .Returns(Task.FromResult(new[] { "itinerary1", "itinerary2" }.AsEnumerable()));

        await _consoleHostedService.StartAsync();
        
        _consoleStringWriter.ToString().Should().Be($"itinerary1{Environment.NewLine}itinerary2");

    }

}