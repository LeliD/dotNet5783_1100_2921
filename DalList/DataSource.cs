
using DO;

namespace Dal;

internal sealed class DataSource
{
    internal List<Product?> ProductsList { get; } = new List<Product?>();
    internal List<Order?> OrdersList { get; } = new List<Order?>();
    internal List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>();
    private static readonly Random s_rand = new();

}
