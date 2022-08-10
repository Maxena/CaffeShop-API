using Microsoft.AspNetCore.Identity;

namespace Caffe.Domain.Entities.Auth;

public class ApplicationUserLogin : IdentityUserLogin<Guid>, IBaseAuditableEntity
{
    public virtual ApplicationUser User { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}