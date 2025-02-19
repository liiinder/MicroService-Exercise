using System.Collections.Generic;

public interface IBookingRepository
{
	IEnumerable<Booking> GetAll();
	Booking? GetById(int id);
	void Add(Booking booking);
	void Update(Booking booking);
	void Delete(int id);
}