using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel
{
    public interface IPublicKeyEntity
    {
        Guid EntityPublicKey { get; set; }
    }
}
