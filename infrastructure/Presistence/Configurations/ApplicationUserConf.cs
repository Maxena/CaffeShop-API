using Caffe.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caffe.Infrastructure.Presistence.Configurations;

internal class ApplicationUserConf : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne(x => x.City)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.CityId);
    }
}