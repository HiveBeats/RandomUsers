using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Features.RandomUsers.Requests;
using WebApi.Features.RandomUsers.Responses;
using WebApi.Features.RandomUsers.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomUserController : ControllerBase
    {
        private readonly IRandomUserService _service;

        public RandomUserController(IRandomUserService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<GetRandomUsersResponse>> Get([FromQuery]RandomUsersRequest request)
        {
            if (request == null || !ModelState.IsValid)
                return BadRequest("Incorrect Parameters");

            var result = await _service.GetUsersAsync(request);

            return Ok(result);
        }
    }
}
