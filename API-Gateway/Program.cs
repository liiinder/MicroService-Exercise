var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Minimal Api";
    config.Title ="Minimal Api v1";
    config.Version="v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config => {
        config.DocumentTitle ="MinimalApi";
        config.Path = "/swagger";
        config.DocumentPath ="/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

var httpClient = new HttpClient();

//Customers

app.MapGet("/gateway/customers", async () =>  
{
    return await httpClient.GetStringAsync("http://localhost:5140/customers");
});

app.MapGet("/gateway/customers/{id}", async (int id) =>  
{
    return await httpClient.GetStringAsync($"http://localhost:5140/customers/{id}");
});

app.MapPut("/gateway/customers", async (Customer customer) =>
{
    var response = await httpClient.PutAsJsonAsync("http://localhost:5140/customers", customer);
    return response.IsSuccessStatusCode ? Results.Ok() : Results.Problem("Failed to update customer");
});

app.MapPost("/gateway/customers", async (Customer customer) =>
{
    var response = await httpClient.PostAsJsonAsync("http://localhost:5140/customers", customer);
    return response.IsSuccessStatusCode ? Results.Created() : Results.Problem("Failed to add customer");
});


//Bookings

//Get all
app.MapGet("/gateway/bookings", async () => 
{
    return await httpClient.GetStringAsync("http://localhost:5003/bookings");
});
//Get by id
app.MapPut("/gateway/bookings/{id}", async (int id) => 
{
    return await httpClient.GetStringAsync($"http://localhost:5003/bookings/{id}");
});

//Post
app.MapPost("/gateway/bookings", async (Booking booking) => 
{
    var response = await httpClient.PostAsJsonAsync("http://localhost:5003/bookings", booking);
    return response.IsSuccessStatusCode ? Results.Created() : Results.Problem("Failed to update customer");
});


app.MapDelete("/gateway/bookings/{id}", async (int id) =>
{
    return await httpClient.DeleteAsync($"http://localhost:5003/bookings/{id}");
});


app.Run();

class Customer
{
public int Id { get; set; }
public required string FirstName { get; set; }
public required string LastName { get; set; }
public required string Email { get; set; }
public required string Membership { get; set; }
}

public class Booking {

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int RoomId { get; set; }
    
    public Booking(int id, int customerId, int roomId)
    {
        Id = id;
        CustomerId = customerId;
        RoomId = roomId;
    }
    
app.Run();

