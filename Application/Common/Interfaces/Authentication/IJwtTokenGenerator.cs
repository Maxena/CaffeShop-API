using Caffe.Domain.Entities.Auth;

namespace Caffe.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}