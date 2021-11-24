using Microsoft.Extensions.DependencyInjection;
using WebApi.Features.RandomUsers.Services;
using WebApi.Features.RandomUsers.Services.ExternalApi;

namespace WebApi.Features.RandomUsers
{
    public class Injector: InjectorBase
    {
        public override void Inject(IServiceCollection services)
        {
            services.AddScoped<IRandomUserService, RandomUserService>();
        }
    }
}
