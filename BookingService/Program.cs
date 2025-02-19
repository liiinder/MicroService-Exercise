var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "Minimal API";
    config.Title = "MinimalAPI v1";
    config.Version = "v1";
});

builder.Services.AddSingleton<IBookingRepository, BookingRepository>();

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

app.MapGet("/", () => "Hello World");

app.MapGet("/bookings", (IBookingRepository service) => service.GetAll());

app.MapGet("/bookings/{id}", (IBookingRepository service, int id) => service.GetById(id));

app.MapPost("/bookings", (IBookingRepository service, Booking booking) => service.Add(booking));

app.MapPut("/bookings", (IBookingRepository service, Booking booking) => service.Update(booking));

app.MapDelete("/bookings/{id}", (IBookingRepository service, int id) => service.Delete(id));

app.Run();