using System.ComponentModel;

namespace Chat.Api.Utility.Enums;

public enum UserRole
{
    [Description("admin")]
    Admin,
    
    [Description("user")]
    User
}
