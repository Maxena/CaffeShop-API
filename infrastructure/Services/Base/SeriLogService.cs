using Caffe.Application.Common.Interfaces.Base;
using Serilog;

namespace Caffe.Infrastructure.Services.Base;

public class SeriLogService : ISerilogService
{
    private readonly ILogger _logger;

    public SeriLogService(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Custom Information Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Info(string message, Exception ex)
    {
        _logger.Information($"{message}{Get(ex)}");
    }

    /// <summary>
    /// Custom Warning Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Warn(string message, Exception ex)
    {
        _logger.Warning($"{message}{Get(ex)}");
    }

    /// <summary>
    /// Custom Verbose Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Verbose(string message, Exception ex)
    {
        _logger.Verbose($"{message}{Get(ex)}");
    }

    /// <summary>
    /// Custom Debug Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Debug(string message, Exception ex)
    {
        _logger.Debug($"{message}{Get(ex)}");
    }

    /// <summary>
    /// Custom Error Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Error(string message, Exception ex)
    {
        _logger.Error($"{message}{Get(ex)}");
    }

    /// <summary>
    /// Custom Fetal Logging
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public void Fetal(string message, Exception ex)
    {
        _logger.Fatal($"{message}{Get(ex)}");
    }

    private string Get(Exception exception)
    {
        return exception == null
            ? string.Empty
            : $"\r\n{exception.Message}\r\n{(exception.InnerException != null ? Get(exception.InnerException) : string.Empty)}";
    }
}