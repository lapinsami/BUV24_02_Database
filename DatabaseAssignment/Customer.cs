namespace DatabaseAssignment;

public class Customer(int id, string firstName, string lastName, string email)
{
    public int Id { get; set; } = id;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
}