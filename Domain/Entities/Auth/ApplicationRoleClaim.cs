using Microsoft.AspNetCore.Identity;

namespace Caffe.Domain.Entities.Auth;

public class ApplicationRoleClaim : IdentityRoleClaim<Guid> , IBaseAuditableEntity
{
    public virtual ApplicationRole Role { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}