namespace WebApi.Features.Auth.Responses
{
    public class RegisterResponse
    {
        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}