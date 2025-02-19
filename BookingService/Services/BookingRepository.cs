using System;
using System.Collections.Generic;
using System.Linq;

public class BookingRepository : IBookingRepository
{
    private List<Booking> _bookings = [
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

    public IEnumerable<Booking> GetAll() => _bookings;

	public Booking? GetById(int id)
	{
        return _bookings.FirstOrDefault(o => o.Id == id);
    }

	public void Add(Booking booking) => _bookings.Add(booking);

	public void Update(Booking booking)
	{
        var preupdate = _bookings.FirstOrDefault(o => o.Id == booking.Id);

        if (preupdate is not null)
        {
            preupdate.CustomerId = booking.CustomerId;
            preupdate.RoomId = booking.RoomId;
        }
    }

	public void Delete(int id)
	{
        var toBeDeleted = _bookings.FirstOrDefault(o => o.Id == id);

        if (toBeDeleted is not null) _bookings.Remove(toBeDeleted);
    }
}