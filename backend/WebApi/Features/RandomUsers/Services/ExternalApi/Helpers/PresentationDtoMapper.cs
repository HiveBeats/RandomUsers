using WebApi.Features.RandomUsers.Responses;
using WebApi.Features.RandomUsers.Services.ExternalApi.Models;

namespace WebApi.Features.RandomUsers.Services.ExternalApi.Helpers
{
    public class PresentationDtoMapper
    {
        public static RandomUserPresenationDto Map(Result source)
        {
            return new RandomUserPresenationDto()
            {
                FullName = $"{source.name.title} {source.name.first} {source.name.last}",
                Location = source.location.country,
                Nationality = source.nat,
                Picture = source.picture.thumbnail
            };
        }
    }
}
