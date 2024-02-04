using ProfileViewer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProfileViewer.Infrastructure.Configurations
{
    public class AppPermissionConfiguration : EntityConfiguration<AppPermission>
    {   
        public AppPermissionConfiguration(EntityTypeBuilder<AppPermission> builder)
        {
            builder.ToTable("AppPermissions");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Ignore(x => x.ModificationDate);
        }
    }
}
