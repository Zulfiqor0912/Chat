namespace Chat.Api.Helpers;

public static class StaticHelper
{
    public static string GetFullName(string firstName, string lastName)
    {
        return $"{lastName} {firstName}";
    }
}
