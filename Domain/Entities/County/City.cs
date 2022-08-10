using Caffe.Domain.Entities.Auth;

namespace Caffe.Domain.Entities.County;

public class City : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Slug { get; set; }


    #region Relations

    public virtual ICollection<ApplicationUser> Users { get; set; }

    #endregion
}