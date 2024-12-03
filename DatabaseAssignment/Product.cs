namespace DatabaseAssignment;

public class Product(int id, string name, string category, decimal price)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Category { get; set; } = category;
    public decimal Price { get; set; } = price;
}