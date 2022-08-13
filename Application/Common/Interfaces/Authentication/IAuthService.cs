using Caffe.Application.Common.Interfaces.Base;
using Caffe.Application.Common.Models.Dtos.Auth;
using Caffe.Domain.Entities.Auth;

namespace Caffe.Application.Common.Interfaces.Authentication;

public interface IAuthService : IRepo<ApplicationUser>
{
    Task<RegisterUserRes> RegisterUser(RegisterUserReq req);
}