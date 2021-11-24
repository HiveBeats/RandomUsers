using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Configuration;
using WebApi.Features.FileExport.Services;
using WebApi.Features.RandomUsers.Requests;
using WebApi.Features.RandomUsers.Responses;
using WebApi.Features.RandomUsers.Services.ExternalApi.Helpers;
using WebApi.Features.RandomUsers.Services.ExternalApi.Models;

namespace WebApi.Features.RandomUsers.Services.ExternalApi
{
    public class RandomUserService: IRandomUserService
    {
        private readonly RandomUserServiceConfig _config;
        private readonly IFileExporter _fileExporter;
        //todo: use HttpClientFactory instead
        private static readonly HttpClient _client = new HttpClient();

        public RandomUserService(IOptions<RandomUserServiceConfig> config, IFileExporter fileExporter)
        {
            _config = config.Value;
            _fileExporter = fileExporter;
        }

        private string GenerateCorrelationId()
        {
            return Guid.NewGuid().ToString();
        }

        //todo: move into separate RequestHelper or smth like that
        private async Task<T> GetRequestAsync<T>(string url)
        {
            var responseMessage = await _client.GetAsync(url);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var resultDto = JsonSerializer.Deserialize<T>(content);
            return resultDto;
        }

        private IEnumerable<Result> FilterRandomUsersByLocation(IEnumerable<Result> source, RandomUsersRequest filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Location))
                return source.Where(x => x.location.country == filter.Location);

            return source;
        }

        private async Task<GetRandomUsersResponse> GetUsersAsync(RandomUsersRequest request, string correlation)
        {
            var correlationId = string.IsNullOrWhiteSpace(correlation) ? GenerateCorrelationId() : correlation;

            var requestUrlBuilder = new RequestBuilder(_config.ApiUrl)
                .WithCount(request.Count ?? _config.DefaultUsersToFetchCount)
                .WithFields($"{RequestBuilder.Name}, {RequestBuilder.Location}, {RequestBuilder.Nationality}, {RequestBuilder.Picture}")
                .WithNationality(request.Nationality)
                .ForCorrelation(correlationId);

            var result = await GetRequestAsync<Root>(requestUrlBuilder.Build());

            var items = FilterRandomUsersByLocation(result.results, request);

            return new GetRandomUsersResponse()
            {
                CorrelationId = correlationId,
                Users = items.Select(x => PresentationDtoMapper.Map(x))
            };
        }

        public async Task<GetRandomUsersResponse> GetUsersAsync(RandomUsersRequest request)
        {
            return await GetUsersAsync(request, string.Empty);
        }

        public async Task<Stream> ExportToFileAsync(ExportToFileRequest request)
        {
            var requestResult = await GetUsersAsync(request, request.CorrelationId);
            return _fileExporter.ExportToFile<RandomUserPresenationDto>(requestResult.Users);
        }
    }
}
