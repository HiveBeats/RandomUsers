using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Features.RandomUsers.Requests
{
    public class RandomUsersRequest
    {
        public string Location { get; set; }
        public string Nationality { get; set; }
    }  
}
