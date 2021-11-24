using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Features.RandomUsers.Responses
{
    public class RandomUserPresenationDto
    {
        public string FullName { get; set; }
        public string Location { get; set; } //country, state, city, street (name, number)
        public string Nationality { get; set; }
        public string Picture { get; set; } //thumnail

        public RandomUserPresenationDto(Services.ExternalApi.Models.Result source)
        {
            FullName = $"{source.name.title} {source.name.first} {source.name.last}";
            Location = $"{source.location.country}, {source.location.state}, {source.location.city}, {source.location.street.name}, {source.location.street.number}";
            Nationality = source.nat;
            Picture = source.picture.thumbnail;
        }
    }
    public class GetRandomUsersResponse
    {
        public string CorrelationId { get; set; }
        public IEnumerable<RandomUserPresenationDto> Users { get; set; }
    }
}
