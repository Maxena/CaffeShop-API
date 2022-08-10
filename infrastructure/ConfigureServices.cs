using Caffe.Application.Common.Interfaces.Authentication;
using Caffe.Application.Common.Interfaces.Base;
using Caffe.Application.Common.Interfaces.Files;
using Caffe.Application.Common.Interfaces.Presistence;
using Caffe.Domain.Entities.Auth;
using Caffe.Infrastructure.Identity;
using Caffe.Infrastructure.Presistence;
using Caffe.Infrastructure.Services.Authentication;
using Caffe.Infrastructure.Services.Base;
using Caffe.Infrastructure.Services.Files;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caffe.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("Sql"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped(typeof(IRepo<>), typeof(Repo<>));

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGeneratorService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();


        return services;
    }
}