using System.Net;

namespace Chat.Client.Repositories.Contract;

public interface IChatIntegration
{
    Task<Tuple<HttpStatusCode, object>> GetUserChats();
    Task<Tuple<HttpStatusCode, object>> GetUserChats(Guid toUserId);
    Task<Tuple<HttpStatusCode, object>> GetMessagesOfChat(Guid chatId);
    Task<Tuple<HttpStatusCode, object>> SendTextMessage(Guid chatId, string text);
    //Task<Tuple<HttpStatusCode, object>> SendFileMessage(Guid chatId, string file);
}