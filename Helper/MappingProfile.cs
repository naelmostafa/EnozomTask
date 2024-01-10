using AutoMapper;
using Enozom.Dto;
using Models;

namespace Enozom.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hotel, HotelDto>().ForMember(hotelDto => hotelDto.Rooms, opt => opt.MapFrom(hotel => hotel.Rooms)).ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
        }
        
    }
}