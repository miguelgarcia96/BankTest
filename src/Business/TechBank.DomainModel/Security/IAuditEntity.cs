using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel
{
    public interface IAuditEntity
    {
        DateTime Created { get; set; }
        int CreatedBy { get; set; }
        DateTime Modified { get; set; }
        int ModifiedBy { get; set; }
        DateTime? Deleted { get; set; }
        int? DeletedBy { get; set; }
        DateTime? Locked { get; set; }
        int? LockedBy { get; set; }
    }
}
