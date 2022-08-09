using System.Globalization;
using Caffe.Application.Common.Interfaces.Files;
using CsvHelper;

namespace Caffe.Infrastructure.Services.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    //public byte[] BuildMenuFiles(IEnumerable<MenuRecord> records)
    //{
    //    using var memoryStream = new MemoryStream();
    //    using (var streamWriter = new StreamWriter(memoryStream))
    //    {
    //        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

    //        csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
    //        csvWriter.WriteRecords(records);
    //    }

    //    return memoryStream.ToArray();
    //}
}