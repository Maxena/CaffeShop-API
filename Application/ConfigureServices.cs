using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.MSSqlServer;

namespace Caffe.Application
{
    public static class ConfigureServices
    {
        private static ILogger? _serilog;
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dateTimeNow = DateTime.Now;
            var dayOfWeek = new PersianCalendar().GetDayOfWeek(dateTimeNow);

            _serilog = new LoggerConfiguration()
                .WriteTo.File($"Logs/{dayOfWeek}|{dateTimeNow}.log", rollingInterval: RollingInterval.Hour)
                .WriteTo.MSSqlServer(configuration.GetConnectionString("SeriLog"), sinkOptions: new MSSqlServerSinkOptions
                {
                    AutoCreateSqlTable = true,
                    TableName = "Logging",
                    SchemaName = "SeriLog"
                }, columnOptions: new ColumnOptions
                {
                    Exception = new ColumnOptions.ExceptionColumnOptions
                    {
                        AllowNull = true,
                        ColumnName = "Exception",
                        NonClusteredIndex = false,
                        PropertyName = "Exception"
                    }
                })
                .WriteTo.Console()
                .WriteTo.Seq("http://127.0.0.1:8443")
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .CreateLogger();

            services.AddLogging(
                _ => _.AddSerilog(_serilog));

            
            
            return services;
        }
    }
}
