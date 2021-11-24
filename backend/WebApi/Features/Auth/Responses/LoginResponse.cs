namespace WebApi.Features.Auth.Responses
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Role { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}