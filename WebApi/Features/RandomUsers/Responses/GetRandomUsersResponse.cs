using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Features.RandomUsers.Responses
{
    public class RandomUserPresenationDto
    {
        public string FullName { get; set; }
        public string Location { get; set; }
        public string Nationality { get; set; }
        public string Picture { get; set; }
    }
    public class GetRandomUsersResponse
    {
        public string CorrelationId { get; set; }
        public IEnumerable<RandomUserPresenationDto> Users { get; set; }
    }
}
