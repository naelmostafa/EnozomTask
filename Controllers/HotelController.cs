using AutoMapper;
using Enozom.Dto;
using Interfaces;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace Enozom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController(IHotelRepository hotelRepository, IMapper mapper) : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository = hotelRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HotelDto>))]
        public IActionResult GetHotels()
        {
            IEnumerable<HotelDto> hotels = _mapper.Map<IEnumerable<HotelDto>>(_hotelRepository.GetHotels());
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(hotels);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetHotel(int id)
        {
            HotelDto hotel = _mapper.Map<HotelDto>(_hotelRepository.GetHotel(id));
            return hotel == null ? NotFound() : Ok(hotel);
        }

        [HttpGet("GetByPriceRange/{minPrice}/{maxPrice}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HotelDto>))]
        public IActionResult GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            IEnumerable<HotelDto> hotels = _mapper.Map<IEnumerable<HotelDto>>(_hotelRepository.GetByPriceRange(minPrice, maxPrice));
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(hotels);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteHotel(int id)
        {
            if (!_hotelRepository.DeleteHotel(id))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the hotel {id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateHotel(int id, [FromBody] HotelDto hotel)
        {
            if (hotel == null || id != hotel.Id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_hotelRepository.UpdateHotel(_mapper.Map<Hotel>(hotel)))
            {
                ModelState.AddModelError("", $"Something went wrong updating the hotel {hotel.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(HotelDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateHotel([FromBody] HotelDto hotel)
        {
            if (hotel == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_hotelRepository.CreateHotel(_mapper.Map<Hotel>(hotel)))
            {
                ModelState.AddModelError("", $"Something went wrong saving the hotel {hotel.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok(hotel);
        }

    }
}