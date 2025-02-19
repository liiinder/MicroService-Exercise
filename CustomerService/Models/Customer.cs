public class Customer
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Membership { get; set; }

    public Customer() { }
    get; set; }

public Customer(int id, string firstName, string lastName, string email, string membership)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Membership = membership;
    }
};