using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBank.DomainModel;

namespace TechBank.Data
{    
    public class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository
    {
        private WebAppContext _appContext;
        public BankAccountRepository(WebAppContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }
        public void Update(BankAccount updatedBankAccount)
        {
            _appContext.BankAccounts.Update(updatedBankAccount);
        }

        public IEnumerable<BankAccount> GetCustomerAccounts(string email)
        {
            var customer = _appContext.
                Customers
                .Include(e => e.User)
                .Include(e => e.Accounts)
                .Where(e => e.User.Email == email)
                .FirstOrDefault();

            return customer.Accounts;
        }

        public BankAccount GetById(int id, bool includeTransactions = false)
        {
            IQueryable<BankAccount> command = _appContext.BankAccounts;

            if (includeTransactions) command = command.Include(e => e.Transactions);

            return command
                .Where(e => e.Id == id)
                .FirstOrDefault();
        }
    }
}
