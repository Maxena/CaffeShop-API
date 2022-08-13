namespace Caffe.Domain.Common;

public interface IBaseAuditableEntity
{
    public Guid Id { get; set; }
    DateTime Created { get; set; }
    string? CreatedBy { get; set; }
    DateTime? LastModified { get; set; }
    string? LastModifiedBy { get; set; }
}

public abstract class BaseAuditableEntity : IBaseAuditableEntity
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}