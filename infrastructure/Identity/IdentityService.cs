using System.Security.Claims;
using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Models;
using Caffe.Application.Common.Models.Dtos;
using Caffe.Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Caffe.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _roleManager = roleManager;
    }

    /// <summary>
    /// register new role
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> RegisterRole(RegisterRole dto)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// remove role 
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> RemoveRole(string roleId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// add user to selected role
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<(Result result, string RoleId)> AddUserToRoleAsync(string userId, string roleName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// remove user from selected role
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// checkc if user can login with this credentials
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<CheckLoginResultDto> CheckLoginAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// create identity instance
    /// </summary>
    /// <param name="user"></param>
    /// <param name="authenticationTypes"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationTypes)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// update user 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IdentityResult> UpdateAsync(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// hash password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public string HashPassword(string password)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// find user name of the user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id.ToString() == userId);

        return user.UserName;
    }

    /// <summary>
    /// find user by list of user ids
    /// </summary>
    /// <param name="listUserId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<IEnumerable<ApplicationUser>> FindUserByListIdAsync(string[] listUserId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// create new user in the database
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<(Result Result, string UserId)> CreateUserAsync(RegisterUserDto dto)
    {
        var user = new ApplicationUser
        {
        };

        var result = await _userManager.CreateAsync(user);

        return (result.ToApplicationResult(), user.Id.ToString());
    }

    /// <summary>
    /// check if the given user is in the given role
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    /// <summary>
    /// authorization
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="policyName"></param>
    /// <returns></returns>
    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    /// <summary>
    /// delete user from database with useId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    /// <summary>
    /// delete user from database with user object 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}
