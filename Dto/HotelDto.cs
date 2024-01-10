
namespace Enozom.Dto
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<RoomDto>? Rooms { get; set; }
    }
}