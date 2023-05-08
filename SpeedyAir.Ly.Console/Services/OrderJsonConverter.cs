using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using SpeedyAir.Ly.Console.Model;

namespace SpeedyAir.Ly.Console.Services;

public class OrderJsonConverter : JsonConverter<IEnumerable<Order>>
{
    public override IEnumerable<Order>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonNode = JsonObject.Parse(ref reader, new JsonNodeOptions());

        var orders = new List<Order>();
        foreach (var orderNode in jsonNode.AsObject())
        {
            var order = new Order { Name = orderNode.Key, Destination = orderNode.Value["destination"].ToString()};
            orders.Add(order);
        }

        return orders;
    }

    public override void Write(Utf8JsonWriter writer, IEnumerable<Order> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}