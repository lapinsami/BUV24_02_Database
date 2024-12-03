namespace DatabaseAssignment;

public class Order(Customer customer)
{
    public int Id { get; set; } = -1;
    public Customer Customer { get; set; } = customer;

    public List<Product> Products { get; set; } = [];
}