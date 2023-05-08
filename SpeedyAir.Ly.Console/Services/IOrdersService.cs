using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public interface IOrdersService
{
    Task<IEnumerable<Order>> GetOrders();
}