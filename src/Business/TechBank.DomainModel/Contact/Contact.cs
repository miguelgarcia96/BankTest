using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel
{
    public class Contact : IPublicKeyEntity, IDisplayNameEntity, IAuditEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }

        public string FullName
        {
            get
            {
                var fullName = FirstName;
                fullName += String.IsNullOrEmpty(MiddleName) ? "" : " " + MiddleName;
                fullName += " " + FirstLastName + " " + SecondLastName;
                return String.IsNullOrEmpty(fullName.Trim()) ? String.Empty : fullName.Trim();
            }
        }

        #region IPublicKeyEntity
        public Guid EntityPublicKey { get; set; }
        #endregion

        #region IDisplayNameEntity
        public string DisplayName { get; set; }
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

        public User User { get; set; }
    }
}
