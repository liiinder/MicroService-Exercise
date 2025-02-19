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

app.MapGet("/bookings/{id}", (IBookingRepository service, int id) => {
    var booking = service.GetById(id);
    if (booking is null) return Results.NotFound("Booking not found");
    return Results.Ok(booking);
});

app.MapPost("/bookings", (IBookingRepository service, Booking booking) => {
    var checkBooking = service.GetById(booking.Id);
    if (checkBooking is not null) return Results.Problem("Booking with this id already exists");
    service.Add(booking);
    return Results.Created("Booking created", service.GetById(booking.Id)); //maybe return the input instead?
});

app.MapPut("/bookings", (IBookingRepository service, Booking booking) => {
    var checkBooking = service.GetById(booking.Id);
    if (checkBooking is null) return Results.NotFound("Booking not found");
    service.Update(booking);
    return Results.Ok("Booking updated");
});

app.MapDelete("/bookings/{id}", (IBookingRepository service, int id) => {
    var checkBooking = service.GetById(id);
    if (checkBooking is null) return Results.NotFound("Booking not found");
    service.Delete(id);
    return Results.Ok("Booking deleted");
});

app.Run();