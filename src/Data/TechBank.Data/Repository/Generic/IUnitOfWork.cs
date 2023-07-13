using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.Data
{
    public interface IUnitOfWork
    {
        IBankAccountRepository BankAccounts { get; }
        ICustomerRepository Customers { get; }

        int Save();

        int AuthSave(string email);
    }
}
