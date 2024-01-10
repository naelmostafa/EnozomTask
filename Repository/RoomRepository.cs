using Data;
using Interfaces;
using Models;

namespace Repository
{
    public class RoomRepository(DataContext dataContext) : IRoomRepository
    {
        private readonly DataContext _dataContext = dataContext;
        public bool CreateRoom(Room room)
        {
            _dataContext.Add(room);
            return Save();
        }
        public bool DeleteRoom(int id)
        {
            _dataContext.Remove(GetRoom(id));
            return Save();
        }
        public Room GetRoom(int id)
        {
            return _dataContext.Rooms.FirstOrDefault(room => room.Id == id);
        }

        public ICollection<Room> GetRooms()
        {
            return _dataContext.Rooms.OrderBy(room => room.Id).ToList();
        }
        public ICollection<Room> GetRoomsByHotelId(int hotelId)
        {
            return _dataContext.Rooms.Where(room => room.Hotel.Id == hotelId).OrderBy(room => room.Id).ToList();
        }

        public ICollection<Room> GetRoomsByHotelIdAndPriceRange(int hotelId, decimal minPrice, decimal maxPrice)
        {
            return _dataContext.Rooms.Where(room => room.Hotel.Id == hotelId && room.Price >= minPrice && room.Price <= maxPrice).OrderBy(room => room.Id).ToList();
        }

        public ICollection<Room> GetRoomsByPrice(decimal price)
        {
            return _dataContext.Rooms.Where(room => room.Price == price).OrderBy(room => room.Id).ToList();
        }
        public ICollection<Room> GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _dataContext.Rooms
                // .Include(room => room.Hotel)
                .Where(room => room.Price >= minPrice && room.Price <= maxPrice)
                .OrderBy(room => room.Id)
                .ToList();
        }
        public bool UpdateRoom(Room room)
        {
            _dataContext.Update(room);
            return Save();
        }
        private bool Save()
        {
            return _dataContext.SaveChanges() >= 0;
        }
    }
}