using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel
{
    public class AppCustomer : IPublicKeyEntity, IAuditEntity
    {
        public AppCustomer()
        {
            Accounts = new List<BankAccount>();
        }

        public int Id { get; set; }

        public User User { get; set; }

        public List<BankAccount> Accounts { get; set; }

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
    }
}
