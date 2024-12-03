namespace DatabaseAssignment;

public class Order(int customerId)
{
    public int Id { get; set; } = -1;
    public int CustomerId { get; set; } = customerId;

    public List<int> Products { get; set; } = [];
}