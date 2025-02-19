public class Booking {

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int RoomId { get; set; }
    
    public Booking(int id, int customerId, int roomId)
    {
        Id = id;
        CustomerId = customerId;
        RoomId = roomId;
    }
}