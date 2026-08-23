using Chat.Api.DTOs;
using Chat.Api.Entities;
using Mapster;

namespace Chat.Api.Extentions;

public static class ParseToDtoExtention
{
    public static UserDto ParseToDto(this User user)
    {
        UserDto dto = user.Adapt<UserDto>();
    }
}
    