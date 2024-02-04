using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileViewer.Domain.Entities
{
    public class Role: IdentityRole<Guid>
    {
        [NotMapped]
        public override string? NormalizedName { get; set; }
        [NotMapped]
        public override string? ConcurrencyStamp { get; set; }

        public Role(string name, Guid id)
        {
           Name = name;
           Id = id;
        }
    }
}
