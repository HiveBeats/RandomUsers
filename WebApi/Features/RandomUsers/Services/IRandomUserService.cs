using System;
using System.IO;
using System.Threading.Tasks;
using WebApi.Features.RandomUsers.Requests;
using WebApi.Features.RandomUsers.Responses;

namespace WebApi.Features.RandomUsers.Services
{
    public interface IRandomUserService
    {
        Task<GetRandomUsersResponse> GetUsersAsync(RandomUsersRequest request);
        Task<Stream> ExportToFileAsync(ExportToFileRequest request);
    }
}
