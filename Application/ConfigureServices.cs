using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.MSSqlServer;

namespace Caffe.Application
{
    public static class ConfigureServices
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dateTimeNow = DateTime.Now;
            var dayOfWeek = new PersianCalendar().GetDayOfWeek(dateTimeNow);

            const string outPutTemplate = "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine($"C:\\Logs\\CaffeShop||{dateTimeNow:yy-MM-dd}||{dayOfWeek}.log"),
                    rollingInterval: RollingInterval.Hour,
                    fileSizeLimitBytes: 100000,
                    outputTemplate: outPutTemplate)
                .WriteTo.MSSqlServer(
                    configuration.GetConnectionString("SeriLog"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        AutoCreateSqlTable = true,
                        TableName = "Logging",
                        SchemaName = "SeriLog"
                    },
                    columnOptions: new ColumnOptions
                    {
                        Exception = new ColumnOptions.ExceptionColumnOptions
                        {
                            AllowNull = true,
                            ColumnName = "Exception",
                            NonClusteredIndex = false,
                            PropertyName = "Exception"
                        }
                    })
                .WriteTo.Console(outputTemplate: outPutTemplate)
                .WriteTo.Seq("http://116.203.234.94:5341")
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .CreateLogger();

            //services.AddLogging(
            //    _ => _.AddSerilog(_serilog));

            services.AddSingleton(Log.Logger);


            return services;
        }
    }
}
