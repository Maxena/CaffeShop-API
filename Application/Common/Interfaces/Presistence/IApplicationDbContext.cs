using Caffe.Domain.Entities.County;
using Microsoft.EntityFrameworkCore;

namespace Caffe.Application.Common.Interfaces.Presistence;

public interface IApplicationDbContext
{
    public DbSet<City> Cities { get; set; }
    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}