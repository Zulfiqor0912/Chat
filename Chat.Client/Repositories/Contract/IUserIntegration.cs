using Chat.Client.Models;
using Chat.Client.Models.UserModels;
using System.Net;

namespace Chat.Client.Repositories.Contract;

public interface IUserIntegration
{
    Task<Tuple<HttpStatusCode, string>> Login(LoginModel model);
    Task<Tuple<HttpStatusCode, string>> Register(RegisterModel model);
}
