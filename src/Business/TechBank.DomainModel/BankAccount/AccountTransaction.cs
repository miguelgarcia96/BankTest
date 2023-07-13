using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel
{
    public class AccountTransaction : IPublicKeyEntity, IAuditEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public string Comments { get; set; }

        public int SourceId { get; set; }
        public virtual BankAccount Source { get; set; }
        public int DestinationId { get; set; }
        public virtual BankAccount Destination { get; set; }

        public int AccountTransactionTypeId { get; set; }

        public AccountTransactionType Type
        {
            get
            {
                return (AccountTransactionType)AccountTransactionTypeId;
            }
        }

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
