using Microsoft.EntityFrameworkCore;
using Data;
using Interfaces;
using Models;

namespace Repository
{
    public class HotelRepository(DataContext dataContext) : IHotelRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public bool CreateHotel(Hotel hotel)
        {
            _dataContext.Hotels.Add(hotel);
            return Save();
        }
        public bool DeleteHotel(int id)
        {
            var hotel = GetHotel(id);
            if (hotel != null)
            {
                _dataContext.Hotels.Remove(hotel);
                return Save();
            }
            return false;
        }

        public ICollection<Hotel> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var hotels = _dataContext.Hotels
                .Select(hotel => new
                {
                    Hotel = hotel,
                    Rooms = hotel.Rooms.Where(room => room.Price >= minPrice && room.Price <= maxPrice)
                })
                .Where(hotel => hotel.Rooms.Any())
                .ToList()
                .Select(hotel =>
                {
                    hotel.Hotel.Rooms = hotel.Rooms.ToList();
                    return hotel.Hotel;
                })
                .ToList();

            return hotels;
        }
        public Hotel GetHotel(int id)
        {
            return _dataContext.Hotels.FirstOrDefault(hotel => hotel.Id == id);
        }

        public ICollection<Hotel> GetHotels()
        {
            return _dataContext.Hotels
                .Include(hotel => hotel.Rooms)
                .OrderBy(h => h.Id).ToList();
        }

        public bool UpdateHotel(Hotel hotel)
        {
            _dataContext.Update(hotel);
            return Save();
        }

        private bool Save()
        {
            return _dataContext.SaveChanges() >= 0;
        }
    }
}