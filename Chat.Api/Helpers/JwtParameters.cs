namespace Chat.Api.Helpers
{
    public class JwtParameters
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
