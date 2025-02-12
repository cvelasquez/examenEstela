public class OrderService
{
    private static List<Order> _orders = new List<Order>();
    private readonly ProductService _productService;

    public OrderService(ProductService productService)
    {
        _productService = productService;
    }

    public Order CreateOrder(Dictionary<int, int> productQuantities)
    {
        var order = new Order { Id = _orders.Count + 1 };

        foreach (var item in productQuantities)
        {
            var productId = item.Key;
            var quantity = item.Value;

            var product = _productService.GetProduct(productId);
            if (product == null)
                throw new ArgumentException($"Producto con ID {productId} no encontrado.");

            if (product.Stock < quantity)
                throw new InvalidOperationException($"No hay suficiente stock para el producto {product.Name}.");

            _productService.ReduceStock(productId, quantity);

            order.Items.Add(new OrderItem
            {
                ProductId = productId,
                Product = product,
                Quantity = quantity
            });
        }

        _orders.Add(order);
        return order;
    }

    public List<Order> GetOrders()
    {
        return _orders;
    }
}