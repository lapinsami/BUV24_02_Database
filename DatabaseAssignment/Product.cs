namespace DatabaseAssignment;

public class Product(string name, string category, decimal price)
{
    public int Id { get; set; } = -1;
    public string Name { get; set; } = name;
    public string Category { get; set; } = category;
    public decimal Price { get; set; } = price;
}