using System.Security.Claims;

namespace Caffe.Application.Common.Interfaces.Authentication;

public interface ICurrentUser
{
    string? UserId { get; }

    string UserName { get; }

    string[] Roles { get; }
}