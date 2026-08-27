using Chat.Api.DTOs;
using Chat.Api.Entities;
using Mapster;
using System.Linq;

namespace Chat.Api.Extentions;

public static class ParseToDtoExtention
{
    public static UserDto ParseUserToDto(this User user)
    {
        UserDto dto = user.Adapt<UserDto>();
        return dto;
    }

    public static List<UserDto> ParseUserDtos(this List<User>? users)
    {
        var dtos = new List<UserDto>();
        if (users is null || users.Count == 0) 
            return dtos;
        dtos.AddRange(users.Select(user => user.ParseUserToDto()));
        return dtos;
    }

    public static async Task<List<ChatDto>> ParseChatDtos(this List<Entities.Chat>? chats)
    {
        var dtos = new List<ChatDto>();
        if (chats is null || chats.Count == 0)
            return dtos;
        await dtos.AddRange(chats.Select(ch => ch.ParseToChatDto()));
        return dtos;
    }

    public static async Task<ChatDto> ParseChatToDto(this Chat chat)
    {
        var dto = UseDestinationValue.
    }
}


    