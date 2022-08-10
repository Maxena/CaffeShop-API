namespace Caffe.Application.Common.Interfaces.Base;

public interface ISerilogService
{
    void Info(string message, Exception ex);
}