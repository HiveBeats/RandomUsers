namespace WebApi.Features.Auth.Responses
{
    public class JwtAuthResult
    {   
        public string AccessToken { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}