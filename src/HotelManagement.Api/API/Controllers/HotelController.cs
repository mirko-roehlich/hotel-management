using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Business;
using HotelManagement.Api.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController(IHotelService hotelService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDto>>> GetAllHotels()
    {
        var hotels = await hotelService.GetAllHotels();
        var hotelDtos = hotels.Select(HotelDto.From);
        return Ok(hotelDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HotelDto>> GetHotelById(int id)
    {
        var hotel = await hotelService.GetHotelById(id);
        if (hotel is null)
        {
            return NotFound();
        }

        return Ok(HotelDto.From(hotel));
    }

    [HttpPost]
    public async Task<ActionResult<HotelDto>> AddHotel([FromBody] CreateHotelRequestDto? dto)
    {
        if (dto is null)
        {
            return BadRequest();
        }

        try
        {
            CreateHotelRequest createHotelRequest = new(dto.Name);
            var hotel = await hotelService.AddHotel(new CreateHotelRequest(createHotelRequest.Name));
            var hotelDto = HotelDto.From(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotelDto.Id }, hotelDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelRequestDto? dto)
    {
        if (dto is null)
        {
            return BadRequest();
        }

        try
        {
            UpdateHotelRequest updateHotelRequest = new(dto.Name);
            var hotel = await hotelService.UpdateHotel(id, updateHotelRequest);
            var hotelDto = HotelDto.From(hotel);

            return Ok(hotelDto);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        try
        {
            await hotelService.DeleteHotel(id);
            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}