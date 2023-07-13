using Microsoft.AspNetCore.Identity;

namespace TechBank.DomainModel
{
    public class User : IdentityUser<int>, IPublicKeyEntity, IAuditEntity
    {
        public override int Id { get; set; }

        #region IPublicKeyEntity
        public Guid EntityPublicKey { get; set; }
        #endregion

        #region IAuditEntity
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Modified { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? Deleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? Locked { get; set; }
        public int? LockedBy { get; set; }
        #endregion

        public Contact Contact { get; set; }
    }
}
