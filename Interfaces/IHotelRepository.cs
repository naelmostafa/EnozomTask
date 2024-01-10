using Models;

namespace Interfaces
{
    public interface IHotelRepository
    {
        public bool CreateHotel(Hotel hotel);
        public ICollection<Hotel> GetHotels();
        public Hotel GetHotel(int id);
        public bool UpdateHotel(Hotel hotel);
        public bool DeleteHotel(int id);
        public ICollection<Hotel> GetByPriceRange(decimal minPrice, decimal maxPrice);
    }
}