using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.API.Extensions;
using HotelManagement.Api.Business;
using HotelManagement.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpGet("{bookingId}")]
    public async Task<ActionResult<BookingResultDto>> GetBookingById(string bookingId)
    {
        try
        {
            var internalBookingId = BookingId.TryParse(bookingId);
            var booking = await bookingService.GetBookingById(internalBookingId);
            if (booking is null)
            {
                return NotFound();
            }

            var bookingDto = BookingResultDto.From(booking);
            return Ok(bookingDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
