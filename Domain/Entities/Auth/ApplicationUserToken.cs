using Microsoft.AspNetCore.Identity;

namespace Caffe.Domain.Entities.Auth;

public class ApplicationUserToken : IdentityUserToken<Guid> , IBaseAuditableEntity
{
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}