using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBank.DomainModel;

namespace TechBank.Data
{    
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        void Update(BankAccount updatedBankAccount);
        IEnumerable<BankAccount> GetCustomerAccounts(string email);
        BankAccount GetById(int id, bool includeTransactions = false);
    }
}
