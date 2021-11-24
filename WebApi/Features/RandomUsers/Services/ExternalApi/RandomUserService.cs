using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Configuration;
using WebApi.Features.RandomUsers.Requests;
using WebApi.Features.RandomUsers.Responses;
using WebApi.Features.RandomUsers.Services.ExternalApi.Helpers;
using WebApi.Features.RandomUsers.Services.ExternalApi.Models;

namespace WebApi.Features.RandomUsers.Services.ExternalApi
{
    public class RandomUserService: IRandomUserService
    {
        private readonly RandomUserServiceConfig _config;
        private static readonly HttpClient _client = new HttpClient();

        public RandomUserService(IOptions<RandomUserServiceConfig> config)
        {
            _config = config.Value;
        }

        private string GenerateCorrelationId()
        {
            return Guid.NewGuid().ToString();
        }

        private async Task<T> GetRequestAsync<T>(string url)
        {
            var responseMessage = await _client.GetAsync(url);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var resultDto = JsonSerializer.Deserialize<T>(content);
            return resultDto;
        }

        public async Task<GetRandomUsersResponse> GetUsersAsync(RandomUsersRequest request)
        {
            var correlationId = GenerateCorrelationId();
            var requestUrlBuilder = new RequestBuilder(_config.ApiUrl)
                .WithCount(_config.DefaultUsersToFetchCount)
                .IncludingFields($"{RequestBuilder.Name}, {RequestBuilder.Location}, {RequestBuilder.Nationality}, {RequestBuilder.Picture}")
                .ForNationality(request.Nationality)
                .ForCorrelation(correlationId);

            //request
            var result = GetRequestAsync<Root>(requestUrlBuilder.Build());
            
            //filter country
            if (string.IsNullOrWhiteSpace(request.Location))
                return await Task.FromResult<GetRandomUsersResponse>(null);
        }
    }
}
