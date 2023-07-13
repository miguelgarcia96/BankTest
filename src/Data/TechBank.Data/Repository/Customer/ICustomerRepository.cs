using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBank.DomainModel;

namespace TechBank.Data
{    
    public interface ICustomerRepository : IRepository<AppCustomer>
    {
        void Update(AppCustomer updatedCustomer);

        IEnumerable<AppCustomer> GetAllCustomers(bool includeUser = false, bool includeContact = false);
        
        AppCustomer? GetByPublicKey(string id, bool includeAccounts = false);

        AppCustomer? GetByEmail(string email, bool includeAccounts = false);
    }
}
