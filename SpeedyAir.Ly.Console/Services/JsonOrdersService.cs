using System.Collections;
using System.Text.Json;
using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public class JsonOrdersService : IOrdersService
{
    private string _fileName;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public JsonOrdersService(string fileName)
    {
        _fileName = fileName;
        _jsonSerializerOptions = new JsonSerializerOptions();
        _jsonSerializerOptions.Converters.Add(new OrderJsonConverter());
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        await using FileStream fileStream = File.OpenRead(_fileName);

        return await JsonSerializer.DeserializeAsync<IEnumerable<Order>>(fileStream, _jsonSerializerOptions) ?? throw new InvalidOperationException();
    }
}