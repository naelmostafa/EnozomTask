namespace Models
{
    public class Room
    {
        public int Id { get; set; }
        public string?  Name { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}