using System.Collections.Generic;
public class CustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new() {
    new Customer{ Id =1, FirstName = "Adam", LastName="Agerling", Email="Adam.Agerling@Swagmail.com", Membership="Premium"},
    new Customer{ Id =2, FirstName = "Johannes", LastName="Breitfeldt", Email="Johannes.Breitfeldt@Swagmail.com", Membership="Gold"},
    new Customer{ Id =3, FirstName = "Kristoffer", LastName="Liiinder", Email="Kristoffer.Liiinder@Swagmail.com", Membership="Silver"},
    };

    public IEnumerable<Customer> GetAll()
    {
        return _customers;
    }

    public Customer? GetById(int id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    public void Add(Customer customer)
    {
        _customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        var existingCustomer = _customers.FirstOrDefault(c => c.Id == customer.Id);
        if (existingCustomer != null)
        {
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Membership = customer.Membership;
        }
    }

    public void Delete(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);

        if (customer != null)
        {
            _customers.Remove(customer);
        }
    }
};