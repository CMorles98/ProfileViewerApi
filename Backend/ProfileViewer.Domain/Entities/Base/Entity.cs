using System.Diagnostics.CodeAnalysis;

namespace ProfileViewer.Domain.Entities.Base
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? ModificationDate { get; private set; }


        [SetsRequiredMembers]
        public Entity(Guid? id = null, DateTime? creationDate = null, DateTime? modificationDate = null) =>
            (Id, CreationDate, ModificationDate) = (id ?? Guid.NewGuid(), creationDate ?? DateTime.Now, modificationDate);
    }
}
