namespace DatabaseAssignment;

public class Order(Customer customer, Product product)
{
    public int Id { get; set; } = -1;
    public int CustomerId { get; set; } = customer.Id;
    public int ProductId { get; set; } = product.Id;
}