var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Customer> customers = new () 
{
    new Customer{ Id =1, FirstName = "Adam", LastName="Agerling", Email="Adam.Agerling@Swagmail.com", Membership="Premium"},
    new Customer{ Id =2, FirstName = "Johannes", LastName="Breitfeldt", Email="Johannes.Breitfeldt@Swagmail.com", Membership="Gold"},
    new Customer{ Id =3, FirstName = "Kristoffer", LastName="Liiinder", Email="Kristoffer.Liiinder@Swagmail.com", Membership="Silver"},
};

app.MapGet("/customers", () => customers );

app.MapGet("/customers/{id}", (int id) => 
{
   var customer = customers.FirstOrDefault(c => c.Id == id);
    if(customer == null) 
    {
     return Results.NotFound("Customer not found");
    }
   return Results.Ok(customer);
} );

app.MapPost("/customers", (Customer customer) => 
{
    customers.Add(customer);
    return Results.Created("/customers/{customer.id}", customer);
} );

app.MapPut("/customers/{id}", (int id, Customer updatedCustomer) => {
    var customer = customers.FirstOrDefault(c => c.Id == id);
    if (customer is null) return Results.NotFound("The user was not found");
    customer.FirstName = updatedCustomer.FirstName;
    customer.LastName = updatedCustomer.LastName;
    customer.Email = updatedCustomer.Email;
    customer.Membership = updatedCustomer.Membership; 
    return Results.Ok();
});

app.MapDelete("/customers/{id}", (int id) => {
    var customer = customers.FirstOrDefault(c => c.Id == id);

    customers.Remove(customer);
    return Results.Ok(id);
    });


app.Run();

//test
class Customer
{
public int Id { get; set; }
public required string FirstName { get; set; }
public required string LastName { get; set; }
public required string Email { get; set; }
public required string Membership { get; set; }
}