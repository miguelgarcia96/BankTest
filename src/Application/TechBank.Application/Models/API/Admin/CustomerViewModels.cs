using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TechBank.Application.Models.API.Customer;

namespace TechBank.Application.Models.API.Admin
{
    //[BindProperties(SupportsGet = true)]
    public class GetCustomerAccountsVM
    {
        [Required]
        public string Email { get; set; }
    }

    public class CreateBankAccountVM : CreateBankAccountBaseVM
    {
        [Required]
        public string Email { get; set; }
    }
}
