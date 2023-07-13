using System.ComponentModel.DataAnnotations;
using TechBank.DomainModel;

namespace TechBank.Application.Models.API.Customer
{
    public class CreateBankAccountBaseVM
    {
        [Required]
        public string Alias { get; set;}

        [Required]
        public decimal? InitialAmount { get; set; }

        [Required]
        public int? BankAccountTypeId { get; set; }

        public BankAccount GetBankAccount()
        {
            return new BankAccount
            {
                EntityPublicKey = Guid.NewGuid(),
                Alias = Alias,
                Balance = InitialAmount.Value,
                BankAccountTypeId = BankAccountTypeId.Value,
            };
        }             
    }

    public class GetAccountVM
    {
        [Required]
        public string Id { get; set; }
    }
}
