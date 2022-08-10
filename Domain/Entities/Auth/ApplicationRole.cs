using Microsoft.AspNetCore.Identity;

namespace Caffe.Domain.Entities.Auth;

public class ApplicationRole : IdentityRole<Guid>, IBaseAuditableEntity
{
    public ApplicationRole()
    {
    }
    public ApplicationRole(string name)
        : base(name)
    {
    }

    public string Description { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    #region Relations

    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }


    #endregion

}