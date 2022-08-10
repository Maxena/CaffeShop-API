using Caffe.Application.Common.Interfaces.Base;
using Serilog;

namespace Caffe.Infrastructure.Services.Base;

public class SeriLogService : ISerilogService
{
    private readonly ILogger _logger;

    public SeriLogService(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    /// <summary>
    /// Custom Information Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Info(string message, Exception? ex)
    {
        _logger.Information(ex is not null ? $"{message}{Get(ex)}" : message);
    }

    /// <summary>
    /// Custom Warning Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Warn(string message, Exception? ex)
    {
        _logger.Warning(ex is not null ? $"{message}{Get(ex)}" : message);
    }

    /// <summary>
    /// Custom Verbose Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Verbose(string message, Exception? ex)
    {
        _logger.Verbose(ex is not null ? $"{message}{Get(ex)}" : message);
    }

    /// <summary>
    /// Custom Debug Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Debug(string message, Exception? ex)
    {
        _logger.Debug(ex is not null ? $"{message}{Get(ex)}" : message);
    }

    /// <summary>
    /// Custom Error Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Error(string message, Exception? ex)
    {
        _logger.Error(ex is not null ? $"{message}{Get(ex)}" : message);
    }

    /// <summary>
    /// Custom Fetal Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Fetal(string message, Exception? ex)
    {
        _logger.Fatal(ex is not null ? $"{message}{Get(ex)}" : message);
    }

    private static string Get(Exception exception)
    {
        return exception == null
            ? string.Empty
            : $"\r\n{exception.Message}\r\n{(exception.InnerException != null ? Get(exception.InnerException) : string.Empty)}";
    }
}