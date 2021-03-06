using System.Collections.Generic;
using System.IO;

namespace WebApi.Features.FileExport.Services
{
    public interface IFileExporter
    {
        Stream ExportToFile<T>(IEnumerable<T> items);
        Stream ExportToFile<T>(T obj);
    }
}
