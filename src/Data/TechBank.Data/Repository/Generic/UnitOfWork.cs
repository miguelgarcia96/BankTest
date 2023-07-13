using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private WebAppContext _webAppContext;
        public IBankAccountRepository BankAccounts { get; private set; }
        public ICustomerRepository Customers { get; private set; }

        public UnitOfWork(WebAppContext webAppContext)
        {
            _webAppContext = webAppContext;
            BankAccounts = new BankAccountRepository(webAppContext);
            Customers = new CustomerRepository(webAppContext);
        }

        public int Save()
        {
            return _webAppContext.SaveChanges();
        }

        public int AuthSave(string email)
        {
            return _webAppContext.SaveChanges(email);
        }
    }
}
