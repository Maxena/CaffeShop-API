namespace Caffe.Application.Common.Interfaces.Base;

public interface ISerilogService
{
    void Info(string message, Exception? ex);
    void Warn(string message, Exception? ex);
    void Verbose(string message, Exception? ex);
    void Debug(string message, Exception? ex);
    void Error(string message, Exception? ex);
    void Fetal(string message, Exception? ex);
}