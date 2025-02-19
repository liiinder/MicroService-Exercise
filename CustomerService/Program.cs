var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Minimal API";
    config.Title = "MinimalAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "MinimalAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}



app.MapGet("/customers", (ICustomerRepository customers) => customers.GetAll());

app.MapGet("/customers/{id}", (int id, ICustomerRepository customers) => {
    var customer = customers.GetById(id);
    if (customer is null) 
    {
        return Results.NotFound("The user was not found");
    }
    return Results.Ok(customer);
});

app.MapPost("/customers", (Customer customer, ICustomerRepository customers) => {
    customers.Add(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapPut("/customers/{id}", (int id, Customer updatedCustomer, ICustomerRepository customers) => {
    var existingCustomer = customers.GetById(id);
    if (existingCustomer is null)
    {
        return Results.NotFound("The user was not found");
    }
    customers.Update(updatedCustomer);
    return Results.Ok(updatedCustomer);
});


app.MapDelete("/customers/{id}", (int id, ICustomerRepository customers) => {
    var existingCustomer = customers.GetById(id);
    if (existingCustomer is null)
    {
        return Results.NotFound("The user was not found");
    }
    customers.Delete(id);
    return Results.Ok("The user was deleted");
});

app.Run();

