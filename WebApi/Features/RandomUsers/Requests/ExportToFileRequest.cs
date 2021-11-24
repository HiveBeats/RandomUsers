using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Features.RandomUsers.Requests
{
    public class ExportToFileRequest: RandomUsersRequest
    {
        [Required]
        public string CorrelationId { get; set; }
    }
}
