using FluentValidation;
using HotelManagement.Api.API.Dtos;

namespace HotelManagement.Api.API.Validators;

public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequestDto>
{
    public CreateRoomRequestValidator()
    {
        RuleFor(x => x.RoomNumber)
            .NotNull()
            .Must(x => x.ToString().Length <= 5 && x.ToString().Length >= 4)
            .WithMessage("Room number should consist of 4 or 5 digits in the format floor + room number");
    }
}