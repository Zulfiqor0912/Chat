using System.Net;

namespace Chat.Client.Repositories.Contract;

public interface IChatIntegration
{
    Task<Tuple<HttpStatusCode, object>> GetUserChats();
}