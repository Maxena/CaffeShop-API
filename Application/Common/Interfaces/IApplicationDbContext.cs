using Caffe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Caffe.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; set; }
    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}