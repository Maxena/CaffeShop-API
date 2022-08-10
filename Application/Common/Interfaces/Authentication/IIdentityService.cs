using System.Security.Claims;
using Caffe.Application.Common.Models;
using Caffe.Application.Common.Models.Dtos;
using Caffe.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace Caffe.Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<bool> RegisterRole(RegisterRole dto);
    Task<bool> RemoveRole(string roleId);
    Task<(Result result, string RoleId)> AddUserToRoleAsync(string userId, string roleName);
    Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName);
    Task<CheckLoginResultDto> CheckLoginAsync(string userName, string password);
    Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationTypes);
    Task<IdentityResult> UpdateAsync(ApplicationUser user);
    string HashPassword(string password);
    Task<string> GetUserNameAsync(string userId);
    Task<IEnumerable<ApplicationUser>> FindUserByListIdAsync(string[] listUserId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<(Result Result, string UserId)> CreateUserAsync(RegisterUserDto user);
    Task<Result> DeleteUserAsync(string userId);

}
