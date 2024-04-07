using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Business;
using HotelManagement.Api.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.API.Controllers;

[Route("api/hotel/{hotelId:int}/[controller]")]
[ApiController]
public class RoomController(IRoomService roomService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRooms(int hotelId)
    {
        var rooms = await roomService.GetAllRooms(hotelId);
        var roomDtos = rooms.Select(RoomDto.From);
        return Ok(roomDtos);
    }

    [HttpGet("{roomId:int}")]
    public async Task<IActionResult> GetRoomById(int hotelId, int roomId)
    {
        try
        {
            var room = await roomService.GetRoomById(hotelId, roomId);
            var roomDto = RoomDto.From(room);
            return Ok(roomDto);
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom(int hotelId, [FromBody] CreateRoomRequestDto? dto)
    {
        if (dto is null)
        {
            return BadRequest();
        }

        try
        {
            CreateRoomRequest createRoomRequest = new(dto.RoomNumber, dto.CategoryId, dto.Capacity, dto.Price);
            var room = await roomService.AddRoom(hotelId, createRoomRequest);
            var roomDto = RoomDto.From(room);
            return CreatedAtAction(nameof(GetRoomById), new { hotelId, roomId = roomDto.Id }, roomDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut("{roomId:int}")]
    public async Task<IActionResult> UpdateRoom(int hotelId, int roomId, [FromBody] UpdateRoomRequestDto? dto)
    {
        if (dto is null)
        {
            return BadRequest();
        }

        try
        {
            UpdateRoomRequest updateRoomRequest = new(dto.RoomNumber, dto.CategoryId, dto.Capacity);
            var room = await roomService.UpdateRoom(hotelId, roomId, updateRoomRequest);
            var roomDto = RoomDto.From(room);
            return Ok(roomDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{roomId:int}")]
    public async Task<IActionResult> DeleteRoom(int hotelId, int roomId)
    {
        try
        {
            await roomService.DeleteRoom(hotelId, roomId);
            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}