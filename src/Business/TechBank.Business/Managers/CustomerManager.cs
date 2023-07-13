using TechBank.Data;
using TechBank.DomainModel;
using TechBank.DomainModel.Models.Admin;

namespace TechBank.Business
{
    public  class CustomerManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AppCustomer> GetAll(bool includeUser = false, bool includeContact = false)
        {
            return _unitOfWork.Customers.GetAllCustomers(includeUser, includeContact);
        }

        public void Add(AppCustomer customer)
        {
            _unitOfWork.Customers.Add(customer);
        }

        public User Create(CustomerCreateVM model)
        {
            var dateNow = DateTime.Now;

            var user = CreateUser(dateNow);

            var contact = new Contact
            {
                EntityPublicKey = Guid.NewGuid(),
                FirstName = model.Input.Name,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
            };

            contact.DisplayName = contact.FullName;

            user.Contact = contact;

            var customer = new DomainModel.AppCustomer
            {
                EntityPublicKey = Guid.NewGuid(),
                User = user,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
            };

            var bankAccount = new BankAccount
            {
                EntityPublicKey = Guid.NewGuid(),
                Alias = "Default account",
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
                BankAccountTypeId = (int)BankAccountType.Check,
            };

            customer.Accounts.Add(bankAccount);

            _unitOfWork.Customers.Add(customer);

            return user;
        }

        public AppCustomer? GetByPublicKey(string id, bool fullCustomer)
        {
            return _unitOfWork.Customers.GetByPublicKey(id);
        }

        public AppCustomer? GetByPublicKey(string id)
        {
            return _unitOfWork.Customers.GetFirstOrDefault(e => e.EntityPublicKey == Guid.Parse(id));
        }

        private User CreateUser(DateTime created)
        {
            try
            {
                var user = Activator.CreateInstance<User>();

                user.EntityPublicKey = Guid.NewGuid();
                user.Created = created;
                user.CreatedBy = 1;
                user.Modified = created;
                user.ModifiedBy = 1;

                return user;
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }        
    }
}
