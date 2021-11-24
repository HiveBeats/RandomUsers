using Microsoft.Extensions.DependencyInjection;
using WebApi.Features.FileExport.Services;
using WebApi.Features.FileExport.Services.Csv;

namespace WebApi.Features.FileExport
{
    public class Injector: InjectorBase
    {
        public override void Inject(IServiceCollection services)
        {
            services.AddSingleton<IFileExporter, CsvFileExporterService>();
        }
    }
}
