using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileViewer.Domain.Entities
{
    public class User: IdentityUser<Guid>
    {
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        [NotMapped]
        public override string? NormalizedUserName { get; set; }
        [NotMapped]
        public override string? NormalizedEmail { get; set; }
        [NotMapped]
        public override string? SecurityStamp { get; set; }
        [NotMapped]
        public override bool LockoutEnabled { get; set; }
        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }
        [NotMapped]
        public override string? ConcurrencyStamp { get; set; }
        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }
        [NotMapped]
        public override string? PhoneNumber { get; set; }
        [NotMapped]
        public override string? UserName { get; set; }
    }
}
