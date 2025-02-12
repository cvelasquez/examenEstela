public class ProductService
{
    private static List<Product> _products = new List<Product>();

    public Product CreateProduct(string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del producto no puede estar vacío.");

        if (price <= 0)
            throw new ArgumentException("El precio debe ser mayor a 0.");

        var product = new Product
        {
            Id = _products.Count + 1,
            Name = name,
            Price = price,
            Stock = stock
        };

        _products.Add(product);
        return product;
    }

    public Product GetProduct(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void ReduceStock(int productId, int quantity)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
            throw new ArgumentException("Producto no encontrado.");

        if (product.Stock < quantity)
            throw new InvalidOperationException("No hay suficiente stock.");

        product.Stock -= quantity;
    }
}