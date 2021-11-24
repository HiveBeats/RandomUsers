using System;
using WebApi.Models;

namespace WebApi.Features.Auth.Responses
{
    public class RefreshTokenDto
    {
        public RefreshTokenDto(RefreshToken token)
        {
            UserId = token.AppUserId;
            TokenString = token.TokenString;
            ExpireAt = token.ExpireAt;
        }

        public string UserId { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}