List<Booking> bookings = [
    new Booking(1, 1, 2),
    new Booking(2, 2, 1),
    new Booking(3, 5, 2),
    new Booking(4, 10, 3),
    new Booking(5, 9, 2),
    new Booking(6, 2, 1),
    new Booking(7, 3, 1),
    new Booking(8, 4, 3),
    new Booking(9, 19, 1)
];

var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/bookings", () => bookings);

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