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

app.MapGet("/bookings", (IBookingRepository booking) => booking.GetAll());

app.MapGet("/bookings/{id}", (int id) => {
    var booking = bookings.FirstOrDefault(o => o.Id == id);
    if (booking is null)
    {
        return Results.NotFound("Booking not found");
    }
    return Results.Ok(booking);
});

app.MapPost("/bookings", (Booking booking) => {
    bookings.Add(booking);

    return Results.Created("/bookings/{bookings.id}", booking);
});

app.MapPut("/bookings/{id}", (int id, Booking booking) => {
    var preupdate = bookings.FirstOrDefault(o => o.Id == id);

    if (preupdate is null)
    {
        return Results.NotFound("Booking not found");
    }
    
    preupdate.CustomerId = booking.CustomerId;
    preupdate.RoomId = booking.RoomId;

    return Results.Ok("/bookings/{bookings.id} Updated");
});

app.MapDelete("/bookings/{id}", (int id) =>
{
    var toBeDeleted = bookings.FirstOrDefault(o => o.Id == id);
    if (toBeDeleted is null)
    {
        return Results.NotFound("Booking not found");
    }

    bookings.Remove(toBeDeleted);
    return Results.Ok("Booking deleted");
});

app.Run();