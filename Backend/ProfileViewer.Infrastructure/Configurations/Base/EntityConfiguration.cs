using ProfileViewer.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProfileViewer.Infrastructure.Configurations
{
    public class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).IsUnicode().IsRequired();
            builder.Property(x => x.CreationDate).HasColumnType("Date").IsRequired();
            builder.Property(x => x.ModificationDate).HasColumnType("Date");
        }
    }
}
