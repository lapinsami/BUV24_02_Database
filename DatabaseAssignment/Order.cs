namespace DatabaseAssignment;

public class Order(int customerId, int productId)
{
    public int Id { get; set; } = -1;
    public int CustomerId { get; set; } = customerId;
    public int ProductId { get; set; } = productId;
}