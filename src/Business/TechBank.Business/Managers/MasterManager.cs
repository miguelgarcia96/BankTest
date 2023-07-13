using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBank.Data;

namespace TechBank.Business
{
    public class MasterManager
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public MasterManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private CustomerManager _customerManager = null;
        public CustomerManager CustomerManager
        {
            get
            {
                if (_customerManager != null)
                    return _customerManager;

                return _customerManager = new CustomerManager(_unitOfWork);
            }
        }

        private AccountManager _accountManager = null;
        public AccountManager AccountManager
        {
            get
            {
                if (_accountManager != null)
                    return _accountManager;

                return _accountManager = new AccountManager(_unitOfWork);
            }
        }

        public void Save()
        {
            _unitOfWork.Save();
        }

        public void AuthSave(string email)
        {
            _unitOfWork.AuthSave(email);
        }
    }
}
