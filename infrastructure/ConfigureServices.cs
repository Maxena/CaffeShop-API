using System.Security.Cryptography.X509Certificates;
using System.Text;
using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Interfaces.Base;
using Caffe.Application.Common.Interfaces.Files;
using Caffe.Application.Common.Interfaces.Presistence;
using Caffe.Application.Common.Models;
using Caffe.Domain.Entities.Auth;
using Caffe.Infrastructure.Identity;
using Caffe.Infrastructure.Presistence;
using Caffe.Infrastructure.Services.Authentication;
using Caffe.Infrastructure.Services.Base;
using Caffe.Infrastructure.Services.Files;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Caffe.Infrastructure;

public static class ConfigureServices
{
    private const string Jwt = "ApiSettings:JwtSettings";
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("SqlServer"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped(typeof(IRepo<>), typeof(Repo<>));

        #region Identity

        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        new IdentityBuilder(typeof(ApplicationUser), services)
            .AddSignInManager<SignInManager<ApplicationUser>>();

        services.Configure<IdentityOptions>(option =>
        {
            option.SignIn.RequireConfirmedPhoneNumber = true;
            option.Password.RequireDigit = true;
            option.Password.RequireLowercase = true;
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequireUppercase = true;
            option.Password.RequiredLength = 8;
            option.Password.RequiredUniqueChars = 1;
            option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            option.User.RequireUniqueEmail = true;
        });


        #endregion

        #region Authentication

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["ApiSettings:JwtSettings:Issuer"],
                        ValidAudience = configuration["ApiSettings:JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApiSettings:JwtSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync("مشکل در برقراری ارتباط با سرور.");
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(
                                Result.Failure(
                                    new[]
                                    { "کاربر گرامی شما هنوز اهراز هویت نشده اید!", "نام کاربری یا رمز عبور اشتباه است." }));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(
                                Result.Failure(
                                    new[]
                                    { "کاربر گرامی شما اجازه دسترسی به این صفحه را ندارید!" }));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });

        #endregion

        services.Configure<JwtSetting>(configuration.GetSection(Jwt));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGeneratorService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ISerilogService, SeriLogService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
        services.AddSingleton<IDateTime, DateTimeService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddTransient<IAuthService, AuthService>();
        
        return services;
    }
}