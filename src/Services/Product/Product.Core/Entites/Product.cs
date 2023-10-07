namespace Product.Core.Entites;
public class Products
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Categories { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreationDate { get; set; }
}
