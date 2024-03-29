using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.API.Extensions;
using HotelManagement.Api.Business;
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
            var offerResult = await offerService.GetOffer(offerRequestDto.ToDomain(), hotelId);
            var offerResultDto = OfferResultDto.From(offerResult);
            return Ok(offerResultDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}