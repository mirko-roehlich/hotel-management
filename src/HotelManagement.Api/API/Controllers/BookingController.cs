using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.API.Extensions;
using HotelManagement.Api.Business;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpGet("{bookingId}")]
    public async Task<ActionResult<BookingResultDto>> GetBookingById(Guid bookingId)
    {
        try
        {
            var booking = await bookingService.GetBookingById(bookingId);
            if (booking is null)
            {
                return NotFound();
            }

            var bookingDto = BookingResultDto.From(booking);
            return Ok(bookingDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult<BookingResultDto>> BookRooms(BookRoomsRequestDto dto)
    {
        try
        {
            var bookRoomsRequest = dto.ToDomain();
            var booking = await bookingService.BookRooms(bookRoomsRequest);
            var bookingDto = BookingResultDto.From(booking);
            return Ok(bookingDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
