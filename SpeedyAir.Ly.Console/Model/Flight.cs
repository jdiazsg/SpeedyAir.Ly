namespace SpeedyAir.Ly.Console.Model;

public class Flight
{
    public int Number { get; set; }
    public int Day { get; set; }
    public Plane Plane { get; set; }
    public Airport Departure { get; set; }
    public Airport Arrival { get; set; }
}