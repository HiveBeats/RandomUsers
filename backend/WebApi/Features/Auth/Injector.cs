using Microsoft.Extensions.DependencyInjection;
using WebApi.Features.Auth.Services;

namespace WebApi.Features.Auth
{
    public class Injector: InjectorBase
    {
        public override void Inject(IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}