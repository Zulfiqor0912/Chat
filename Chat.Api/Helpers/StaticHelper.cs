using Chat.Api.Constants;
using Chat.Api.Exceptions;

namespace Chat.Api.Helpers;

public static class StaticHelper
{
    public static string GetFullName(string firstName, string lastName)
    {
        return $"{lastName} {firstName}";
    }

    public static void IsPhoto(IFormFile file) 
    {
        var check = file.ContentType == UserConstants.JpgType
        || file.ContentType == UserConstants.PngType;

        if (!check)
            throw new NotFotoTypeException();
    }
        

    public static byte[] PhotoFileToArray(IFormFile file)
    {
        if (file.Length > 5 * 1024 * 1024) //ms ni array ko'rinishiga o'tkazdim. 5mb gacha byte ko'rinishida saqlayman.
            throw new Exception("File size is larger than 5mb.");

        var ms = new MemoryStream();
        file.CopyToAsync(target: ms); // bu yerda fileni memorystream ga o'tkazib oldik.
        var data = ms.ToArray();
        return data;
    }
}
