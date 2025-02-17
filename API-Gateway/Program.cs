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

app.MapGet("/gateway/customers", async () =>  
{
    return await httpClient.GetStringAsync("http://localhost:5065/customers");
});

app.MapGet("/gateway/customers/{id}", async (int id) =>  
{
    return await httpClient.GetStringAsync($"http://localhost:5065/customers/{id}");
});

app.MapPut("/gateway/customers", async (Customer customer) =>
{
    var response = await httpClient.PutAsJsonAsync("http://localhost:5065/customers", customer);
    return response.IsSuccessStatusCode ? Results.Ok() : Results.Problem("Failed to update customer");
});

app.MapPost("/gateway/customers", async (Customer customer) =>
{
    var response = await httpClient.PutAsJsonAsync("http://localhost:5065/customers", customer);
    return response.IsSuccessStatusCode ? Results.Created() : Results.Problem("Failed to update customer");
});

// app.MapGet("/gateway/bookings", async () => 
// {
//     return await httpClient.GetStringAsync("http://localhost:5140/bookings");
// });
// app.MapPut("/gateway/bookings/{}", async () => 
// {
//     return await httpClient.GetStringAsync("http://localhost:5140/bookings");
// });
// app.MapGet("/gateway/bookings", async () => 
// {
//     return await httpClient.GetStringAsync("http://localhost:5140/bookings");
// });






// AddBooking
// DeleteBooking






app.Run();

class Customer
{
public int Id { get; set; }
public required string FirstName { get; set; }
public required string LastName { get; set; }
public required string Email { get; set; }
public required string Membership { get; set; }
}