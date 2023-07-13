using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBank.DomainModel;

namespace TechBank.Data
{    
    public class CustomerRepository : Repository<AppCustomer>, ICustomerRepository
    {
        private WebAppContext _appContext;
        public CustomerRepository(WebAppContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }
        public void Update(AppCustomer updatedCustomer)
        {
            _appContext.Customers.Update(updatedCustomer);
        }

        public IEnumerable<AppCustomer> GetAllCustomers(bool includeUser = false, bool includeContact = false)
        {
            IQueryable<AppCustomer> command = _appContext.Customers;

            if (includeUser) command = command.Include(e => e.User);

            if (includeUser && includeContact) command = command.Include(e => e.User.Contact);

            return command.ToList();
        }
        
        public AppCustomer? GetByPublicKey(string id, bool includeAccounts = false)
        {
            IQueryable<AppCustomer> command = _appContext
                .Customers
                .Include(e => e.User.Contact);

            if (includeAccounts) command = command.Include(e => e.Accounts);

            return command
                .Where(e => e.EntityPublicKey == Guid.Parse(id))
                .FirstOrDefault();
        }

        public AppCustomer? GetByEmail(string email, bool includeAccounts = false)
        {
            IQueryable<AppCustomer> command = _appContext
                .Customers
                .Include(e => e.User.Contact);

            if (includeAccounts) command = command.Include(e => e.Accounts);

            return command
                .Where(e => e.User.Email == email)
                .FirstOrDefault();
        }
    }
}
