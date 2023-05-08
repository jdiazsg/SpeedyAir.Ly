using CommandLine;

namespace SpeedyAir.Ly.Console;

public class Options
{
    [Option('o', Required = true, HelpText = "Operation to perform.")]
    public Operation Operation { get; set; }

    [Option('f', Required = false, HelpText = "JSON file path containing orders.")]
    public string? FileName { get; set; }
}

public enum Operation
{
    ListFlightSchedule,
    GenerateFlightItineraries
}