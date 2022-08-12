using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper.Configuration;
using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Interfaces.Presistence;
using Caffe.Domain.Entities.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Caffe.Infrastructure.Services.Authentication;

public class JwtTokenGeneratorService : IJwtTokenGenerator
{
    private readonly IDateTime _date;
    private readonly JwtSetting _jwtSetting;
    private readonly IIdentityService _identity;

    public JwtTokenGeneratorService(IOptions<JwtSetting> jwtSetting, IDateTime date, IIdentityService identity)
    {
        _date = date;
        _identity = identity;
        _jwtSetting = jwtSetting.Value;
    }


    public async Task<string> GenerateToken(ApplicationUser user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSetting.Key)),
            SecurityAlgorithms.HmacSha256);

        var claims = await _identity.GetUserClaims(user.Id.ToString());

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: _date.Now.AddMonths(_jwtSetting.ExpireInMonth),
            signingCredentials: signingCredentials);


        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}