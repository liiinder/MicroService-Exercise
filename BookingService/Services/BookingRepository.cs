using System.Collections.Generic;

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

	public Booking? GetById(int id)
	{

	}

	public void Add(Booking booking)
	{

	}

	public void Update(Booking booking)
	{

	}

	public void Delete(int id)
	{

	}

}