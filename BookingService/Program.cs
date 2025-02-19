var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
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

app.MapGet("/bookings", (IBookingRepository service) => service.GetAll());

app.MapGet("/bookings/{id}", (int id, IBookingRepository service) => service.GetById(id));

app.MapPost("/bookings", (Booking booking, IBookingRepository service) => service.Add(booking));

app.MapPut("/bookings/{id}", (Booking booking, IBookingRepository service) => service.Update(booking));

app.MapDelete("/bookings/{id}", (int id, IBookingRepository service) => service.Delete(id));

app.Run();