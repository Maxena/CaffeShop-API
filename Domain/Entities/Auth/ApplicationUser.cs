using Caffe.Domain.Entities.County;
using Caffe.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Caffe.Domain.Entities.Auth;

public class ApplicationUser : IdentityUser, IBaseAuditableEntity
{
    [PersonalData]
    public string FirstName { get; set; }
    [PersonalData]
    public string LastName { get; set; }
    [PersonalData]
    public string SnapShot { get; set; }
    [PersonalData]
    public DateTime DateOfBirth { get; set; }
    [PersonalData]
    public Gender Gender { get; set; }
    public Guid CityId { get; set; }
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    #region Relations
    public virtual City City { get; set; }

    #endregion
}