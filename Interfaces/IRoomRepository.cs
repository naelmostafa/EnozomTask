using Models;

namespace Interfaces
{
    public interface IRoomRepository
    {
        public ICollection<Room> GetRooms();
        public Room GetRoom(int id);
        public bool CreateRoom(Room room);
        public bool UpdateRoom(Room room);
        public bool DeleteRoom(int id);
        public ICollection<Room> GetRoomsByHotelId(int hotelId);
        public ICollection<Room> GetRoomsByHotelIdAndPriceRange(int hotelId, decimal minPrice, decimal maxPrice);
        public ICollection<Room> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice);
        public ICollection<Room> GetRoomsByPrice(decimal price);
    

    }
}