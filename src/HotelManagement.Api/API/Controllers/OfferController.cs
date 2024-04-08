using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.API.Extensions;
using HotelManagement.Api.Business;
using HotelManagement.Api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.API.Controllers;

[Route("api/hotel/{hotelId:int}/[controller]")]
[ApiController]
public class OfferController(IOfferService offerService) : Controller
{
    [HttpPost]
    public async Task<ActionResult<OfferResultDto>> RequestRoomOffer(int hotelId, OfferRequestDto offerRequestDto)
    {
        try
        {
            HotelId id = new(hotelId);
            var offerResult = await offerService.GetOffer(offerRequestDto.ToDomain(), id);
            var offerResultDto = OfferResultDto.From(offerResult);
            return Ok(offerResultDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}