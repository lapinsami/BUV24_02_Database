namespace DatabaseAssignment;

public class Customer(string firstName, string lastName, string email)
{
    public int Id { get; set; } = -1;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
}