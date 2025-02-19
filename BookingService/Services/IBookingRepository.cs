using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

public interface IBookingRepository
{
	IEnumerable<Booking> GetAll();
	Booking? GetById(int id);
	void Add(Booking booking);
	void Update(Booking booking);
	void Delete(int id);
}