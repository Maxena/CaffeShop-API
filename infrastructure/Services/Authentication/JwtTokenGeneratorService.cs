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
    private readonly IConfiguration _configuration;
    private readonly JwtSetting _jwtSetting;

    public JwtTokenGeneratorService(IConfiguration configuration, IOptions<JwtSetting> jwtSetting, IDateTime date)
    {
        _configuration = configuration;
        _date = date;
        _jwtSetting = jwtSetting.Value;
    }


    public string GenerateToken(ApplicationUser user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSetting.Key)),
            SecurityAlgorithms.HmacSha256);


        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier,user.UserName),
            new (ClaimTypes.PrimarySid,user.Id),
            new (ClaimTypes.MobilePhone,user.PhoneNumber),
            new (ClaimTypes.Hash,user.PasswordHash)
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: _date.Now.AddMonths(_jwtSetting.ExpireInMonth),
            signingCredentials: signingCredentials);


        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}