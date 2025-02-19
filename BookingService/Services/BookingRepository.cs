using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

public class BookingRepository : IBookingRepository
{
    private List<Booking> bookings = [
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

    public IEnumerable<Booking> GetAll()
	{
		return bookings;
	}

	public IResult GetById(int id)
	{
        var booking = bookings.FirstOrDefault(o => o.Id == id);
        if (booking is null)
        {
            return Results.NotFound("Booking not found");
        }
        return Results.Ok(booking);
    }

	public IResult Add(Booking booking)
	{
        bookings.Add(booking);

        return Results.Created("/bookings/{bookings.id}", booking);
    }

	public IResult Update(Booking booking)
	{
        var preupdate = bookings.FirstOrDefault(o => o.Id == booking.Id);

        Console.WriteLine($"Does it run here? {booking.Id}");
        if (preupdate is null)
        {
            return Results.NotFound("Booking not found");
        }

        preupdate.CustomerId = booking.CustomerId;
        preupdate.RoomId = booking.RoomId;

        return Results.Ok("/bookings/{bookings.id} Updated");
    }

	public IResult Delete(int id)
	{
        var toBeDeleted = bookings.FirstOrDefault(o => o.Id == id);
        if (toBeDeleted is null)
        {
            return Results.NotFound("Booking not found");
        }

        bookings.Remove(toBeDeleted);
        return Results.Ok("Booking deleted");
    }

}