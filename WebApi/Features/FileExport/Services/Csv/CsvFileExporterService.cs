using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Features.FileExport.Helpers;

namespace WebApi.Features.FileExport.Services.Csv
{
    public class CsvFileExporterService : IFileExporter
    {
        

        public Stream ExportToFile<T>(ICollection<T> items)
        {
            //replace to some library in production scenario
            var type = typeof(T);
            var properties = type.GetProperties().ToDictionary(x => x, x=> x.Name);
            
            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine(string.Join(';', properties.Values));

            foreach (var item in items)
            {
                List<string> values = new List<string>();
                foreach (var property in properties.Keys)
                {
                    values.Add(property.GetValue(item).ToString());
                }
                csvBuilder.AppendLine(string.Join(';', values));
            }

            return StreamHelper.GetStream(csvBuilder.ToString());
        }

        public Stream ExportToFile<T>(T obj)
        {
            var properties = typeof(T).GetProperties().ToDictionary(x => x, x => x.Name);

            var csvBuilder = new StringBuilder();

            List<string> values = new List<string>();
            foreach (var property in properties.Keys)
            {
                values.Add(property.GetValue(obj).ToString());
            }
            csvBuilder.AppendLine(string.Join(';', values));

            return StreamHelper.GetStream(csvBuilder.ToString());
        }
    }
}
