using System.Data;
using System.Reflection;
using Caffe.Application.Common.Interfaces.Presistence;
using Caffe.Domain.Entities.Auth;
using Caffe.Domain.Entities.County;
using Caffe.Infrastructure.Common;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace Caffe.Infrastructure.Presistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    #region Entities
    public DbSet<City> Cities { get; set; }

    #endregion
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(e => e.ToTable("Users"));
        builder.Entity<IdentityRole>(e => e.ToTable("Roles"));
        builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));
        builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    public override int SaveChanges()
    {
        CleanString();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        CleanString();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        CleanString();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        CleanString();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void CleanString()
    {
        var changedEntities = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
        foreach (var item in changedEntities)
        {
            var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                var val = (string)property.GetValue(item.Entity, null)!;

                if (!val.HasValue()) continue;
                var newVal = val.Fa2En().FixPersianChars();
                if (newVal == val)
                    continue;
                property.SetValue(item.Entity, newVal, null);
            }
        }
    }
}