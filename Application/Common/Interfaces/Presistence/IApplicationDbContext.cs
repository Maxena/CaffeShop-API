using Caffe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Caffe.Application.Common.Interfaces.Presistence;

public interface IApplicationDbContext
{
    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}