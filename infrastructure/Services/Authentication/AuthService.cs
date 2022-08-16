using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Interfaces.Presistence;
using Caffe.Application.Common.Models.Dtos.Auth;
using Caffe.Domain.Entities.Auth;
using Caffe.Infrastructure.Identity;
using Caffe.Infrastructure.Presistence;
using Caffe.Infrastructure.Services.Base;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Caffe.Infrastructure.Services.Authentication;

public class AuthService : Repo<ApplicationUser>, IAuthService
{
    #region Construct Area

    private readonly IIdentityService _identityService;
    private readonly IDateTime _date;
    public AuthService(ApplicationDbContext dbContext, IIdentityService identityService, IDateTime date) : base(dbContext)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _date = date ?? throw new ArgumentNullException(nameof(date));
    }

    #endregion

    public async Task<RegisterUserRes> RegisterUser(RegisterUserReq req, string UserId)
    {
        var user = new ApplicationUser
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            UserName = req.UserName,
            Email = req.Email,
            PasswordHash = _identityService.HashPassword(req.Password),
            PhoneNumber = req.PhoneNumber,
            SnapShot = req.SnapShot!.FileName,
            CreatedBy = UserId,
            DateOfBirth = req.DateOfBirth,
            Created = _date.Now,
            EmailConfirmed = false,
            Gender = req.Gender,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false
        };

        throw new NotImplementedException();
    }
}