using System.Reflection;
using Caffe.Application.Common.Interfaces;
using Caffe.Domain.Entities;
using Caffe.Infrastructure.Common;
using Caffe.Infrastructure.Identity;
using Caffe.Infrastructure.Presistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Caffe.Infrastructure.Presistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
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