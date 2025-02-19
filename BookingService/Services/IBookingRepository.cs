using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

public interface IBookingRepository
{
	IEnumerable<Booking> GetAll();
	IResult GetById(int id);
	IResult Add(Booking booking);
	IResult Update(Booking booking);
	IResult Delete(int id);
}