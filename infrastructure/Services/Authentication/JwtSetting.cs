namespace Caffe.Infrastructure.Services.Authentication;

public class JwtSetting
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int ExpireInMonth { get; set; }
}