using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel
{
    public class BankAccount : IPublicKeyEntity, IAuditEntity
    {
        public BankAccount()
        {
            Transactions = new List<AccountTransaction>();
        }

        public int Id { get; set; }        

        public string Alias { get; set; }

        public decimal Balance { get; set; }

        public int BankAccountTypeId { get; set; }

        public BankAccountType Type
        {
            get
            {
                return (BankAccountType)BankAccountTypeId;
            }
        }

        public List<AccountTransaction> Transactions { get; set; }

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
