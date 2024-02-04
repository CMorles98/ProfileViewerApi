using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ProfileViewer.Domain.Entities.Base;

namespace ProfileViewer.Domain.Entities
{
    public class AppPermission: Entity
    {
        [SetsRequiredMembers]
        public AppPermission(string name) => Name = name;

        public required string Name { get; set; }
    }
}
