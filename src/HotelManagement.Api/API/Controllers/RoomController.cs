using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Business;
using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.API.Controllers;

[Route("api/hotel/{hotelId:int}/[controller]")]
[ApiController]
public class RoomController(IRoomService roomService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllRooms(int hotelId)
    {
        HotelId id = new(hotelId);
        var rooms = await roomService.GetAllRooms(id);
        var roomDtos = rooms.Select(RoomDto.From);
        return Ok(roomDtos);
    }

    [HttpGet("{roomId:int}")]
    public async Task<IActionResult> GetRoomById(int hotelId, int roomId)
    {
        try
        {
            HotelId id = new(hotelId);
            var room = await roomService.GetRoomById(id, roomId);
            var roomDto = RoomDto.From(room);
            return Ok(roomDto);
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            HotelId id = new(hotelId);
            var room = await roomService.AddRoom(id, createRoomRequest);
            var roomDto = RoomDto.From(room);
            return CreatedAtAction(nameof(GetRoomById), new { hotelId, roomId = roomDto.Id }, roomDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
            HotelId id = new(hotelId);
            var room = await roomService.UpdateRoom(id, roomId, updateRoomRequest);
            var roomDto = RoomDto.From(room);
            return Ok(roomDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{roomId:int}")]
    public async Task<IActionResult> DeleteRoom(int hotelId, int roomId)
    {
        try
        {
            HotelId id = new(hotelId);
            await roomService.DeleteRoom(id, roomId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}