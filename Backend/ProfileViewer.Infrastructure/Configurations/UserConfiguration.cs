using ProfileViewer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProfileViewer.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnType("Date").IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.ModificationDate).HasColumnType("Date");
        }
    }
}
