using System.Security.Claims;
using Caffe.Application.Common.Interfaces.Authentication;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Http;

namespace Caffe.Infrastructure.Services.Authentication;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContext;

    public CurrentUser(IHttpContextAccessor httpContext)
        => _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
    public string? UserId => _httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string UserName => _httpContext.HttpContext!.User.Identity!.Name!;
    public string[] Roles => _httpContext.HttpContext!.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
}