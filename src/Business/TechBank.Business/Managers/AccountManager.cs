using TechBank.Data;
using TechBank.DomainModel;
using TechBank.DomainModel.Models.API.Customer;

namespace TechBank.Business
{
    public class AccountManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BankAccount> GetCustomerAccounts(string email)
        {
            return _unitOfWork.BankAccounts.GetCustomerAccounts(email);
        }

        public IEnumerable<AccountTransaction> GetAccountTransactions(string id, string email)
        {
            var customer = _unitOfWork.Customers.GetByEmail(email, true);

            var customerAccount = customer.Accounts.FirstOrDefault(e => e.EntityPublicKey == Guid.Parse(id));

            if (customerAccount == null) return null;

            var account = _unitOfWork.BankAccounts.GetById(customerAccount.Id, true);

            return account.Transactions;
        }

        public BankAccount GetBankAccount(string id, string email)
        {
            var customer = _unitOfWork.Customers.GetByEmail(email, true);

            var customerAccount = customer.Accounts.FirstOrDefault(e => e.EntityPublicKey == Guid.Parse(id));

            if (customerAccount == null) return null;

            var account = _unitOfWork.BankAccounts.GetById(customerAccount.Id);

            return account;
        }

        public void CreateBankAccount(BankAccount bankAccount, string email)
        {
            var customer = _unitOfWork.Customers.GetByEmail(email, true);

            customer.Accounts.Add(bankAccount);
        }

        public TransferResponse InternalTranser(TransferVM model, string email)
        {
            TransferResponse transferResponse = new TransferResponse();            

            var customer = _unitOfWork.Customers.GetByEmail(email, true);

            // Get customer accounts for transer
            var sourceAccount = customer.Accounts.FirstOrDefault(e => e.EntityPublicKey == Guid.Parse(model.SourceId));

            var destionationAccount = customer.Accounts.FirstOrDefault(e => e.EntityPublicKey == Guid.Parse(model.DestinationId));

            if(sourceAccount == null || destionationAccount == null)
            {
                transferResponse = new TransferResponse
                {
                    Success = false,
                    Message = "Either source account is wrong or destionation account is wront",
                };
                return transferResponse;
            }            

            return Transfer(sourceAccount, destionationAccount, model.Amount.Value);
        }

        public TransferResponse Transfer(TransferVM model, string email)
        {
            TransferResponse transferResponse = new TransferResponse();

            var customer = _unitOfWork.Customers.GetByEmail(email, true);

            // Get customer accounts for transer
            var sourceAccount = customer.Accounts.FirstOrDefault(e => e.EntityPublicKey == Guid.Parse(model.SourceId));

            var destionationAccount = _unitOfWork.BankAccounts.GetFirstOrDefault(e => e.EntityPublicKey == Guid.Parse(model.DestinationId));

            if (sourceAccount == null || destionationAccount == null)
            {
                transferResponse = new TransferResponse
                {
                    Success = false,
                    Message = "Either source account is wrong or destionation account is wront",
                };
                return transferResponse;
            }

            return Transfer(sourceAccount, destionationAccount, model.Amount.Value);
        }

        public TransferResponse PublicTranser(TransferVM model, string email)
        {
            TransferResponse transferResponse = new TransferResponse();

            var customer = _unitOfWork.Customers.GetByEmail(email, true);

            // Get customer accounts for transer
            var sourceAccount = customer.Accounts.FirstOrDefault(e => e.EntityPublicKey == Guid.Parse(model.SourceId));

            var destionationAccount = _unitOfWork.BankAccounts.GetFirstOrDefault(e => e.EntityPublicKey == Guid.Parse(model.DestinationId));

            if (sourceAccount == null || destionationAccount == null)
            {
                transferResponse = new TransferResponse
                {
                    Success = false,
                    Message = "Either source account is wrong or destionation account is wront",
                };
                return transferResponse;
            }

            return Transfer(sourceAccount, destionationAccount, model.Amount.Value);
        }        

        private TransferResponse Transfer(BankAccount sourceAccount, BankAccount destionationAccount, decimal amount)
        {
            var transferResponse = new TransferResponse
            {
                Success = true,
                Message = "Success!"
            };

            // Validate that source account has funds

            if (sourceAccount.Balance < amount)
            {
                transferResponse = new TransferResponse
                {
                    Success = false,
                    Message = "Not enough funds",
                };
                return transferResponse;
            }

            // Transfer money
            sourceAccount.Balance = sourceAccount.Balance - amount;
            destionationAccount.Balance = destionationAccount.Balance + amount;

            _unitOfWork.BankAccounts.Update(sourceAccount);
            _unitOfWork.BankAccounts.Update(destionationAccount);

            sourceAccount.Transactions.Add(new AccountTransaction
            {
                EntityPublicKey = Guid.NewGuid(),
                Amount = amount,
                SourceId = sourceAccount.Id,
                DestinationId = destionationAccount.Id,
                AccountTransactionTypeId = (int)AccountTransactionType.Withdraw,
            });

            destionationAccount.Transactions.Add(new AccountTransaction
            {
                EntityPublicKey = Guid.NewGuid(),
                Amount = amount,
                SourceId = sourceAccount.Id,
                DestinationId = destionationAccount.Id,
                AccountTransactionTypeId = (int)AccountTransactionType.Deposit,
            });

            return transferResponse;
        }
    }
}
