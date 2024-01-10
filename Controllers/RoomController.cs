using AutoMapper;
using Enozom.Dto;
using Interfaces;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace Enozom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IRoomRepository roomRepository, IMapper mapper) : ControllerBase
    {
        private readonly IRoomRepository _roomRepository = roomRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomDto>))]
        public IActionResult GetRooms()
        {
            IEnumerable<RoomDto> rooms = _mapper.Map<IEnumerable<RoomDto>>(_roomRepository.GetRooms());
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(rooms);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRoom(int id)
        {
            RoomDto room = _mapper.Map<RoomDto>(_roomRepository.GetRoom(id));
            return room == null ? NotFound() : Ok(room);
        }

        [HttpGet("GetRoomsByHotelId/{hotelId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomDto>))]
        public IActionResult GetRoomsByHotelId(int hotelId)
        {
            IEnumerable<RoomDto> rooms = _mapper.Map<IEnumerable<RoomDto>>(_roomRepository.GetRoomsByHotelId(hotelId));
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(rooms);
        }

        [HttpGet("GetRoomsByHotelIdAndPriceRange/{hotelId}/{minPrice}/{maxPrice}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomDto>))]
        public IActionResult GetRoomsByHotelIdAndPriceRange(int hotelId, decimal minPrice, decimal maxPrice)
        {
            IEnumerable<RoomDto> rooms = _mapper.Map<IEnumerable<RoomDto>>(_roomRepository.GetRoomsByHotelIdAndPriceRange(hotelId, minPrice, maxPrice));
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(rooms);
        }

        [HttpGet("GetRoomsByPriceRange/{minPrice}/{maxPrice}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetRoomsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            IEnumerable<RoomDto> rooms = _mapper.Map<IEnumerable<RoomDto>>(_roomRepository.GetRoomsByPriceRange(minPrice, maxPrice));
            return !ModelState.IsValid ? BadRequest(ModelState) : rooms.Any() ? NotFound() : Ok(rooms);
        }

        [HttpGet("GetRoomsByPrice/{price}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomDto>))]
        public IActionResult GetRoomsByPrice(decimal price)
        {
            IEnumerable<RoomDto> rooms = _mapper.Map<IEnumerable<RoomDto>>(_roomRepository.GetRoomsByPrice(price));
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(rooms);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoomDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateRoom([FromBody] RoomDto room)
        {
            if (room == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_roomRepository.CreateRoom(_mapper.Map<Room>(room)))
            {
                ModelState.AddModelError("", $"Something went wrong saving the room {room.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok(room);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateRoom(int id, [FromBody] Room room)
        {
            if (room == null || id != room.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_roomRepository.UpdateRoom(room))
            {
                ModelState.AddModelError("", $"Something went wrong updating the room {room.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteRoom(int id)
        {
            if (!_roomRepository.DeleteRoom(id))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the room {id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}