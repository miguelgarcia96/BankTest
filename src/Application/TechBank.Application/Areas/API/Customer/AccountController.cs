#region LibraryReferences
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBank.Application.Controllers;
using TechBank.Application.Models.API.Customer;
using TechBank.Data;
using TechBank.DomainModel.Models.API.Customer;
#endregion

namespace TechBank.Application.Areas.API.Customer
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Customer")]
    public class AccountController : BaseControllerAPI
    {
        #region DI        
        public AccountController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        [HttpGet]
        public IActionResult GetBankAccounts()
        {
            var customerAccounts = MasterManager.AccountManager.GetCustomerAccounts(RequestEmail(User));
            return Ok(new { Accounts = customerAccounts });
        }

        [HttpPost]
        public IActionResult CreateBankAccount([FromBody] CreateBankAccountBaseVM model)
        {
            if (ModelState.IsValid)
            {
                var bankAccount = model.GetBankAccount();
                MasterManager.AccountManager.CreateBankAccount(bankAccount, RequestEmail(User));
                MasterManager.Save();
                return Ok();
            }

            return BadRequest();

        }

        [HttpPost]
        public IActionResult InternalTransfer([FromBody] TransferVM model)
        {
            if (ModelState.IsValid)
            {
                var response = MasterManager.AccountManager.InternalTranser(model, RequestEmail(User));
                if (response.Success)
                {
                    MasterManager.Save();
                    return Ok(new { Response = response });
                }
                return BadRequest(new { Response = response });
            }

            return BadRequest();

        }

        [HttpPost]
        public IActionResult Transfer([FromBody] TransferVM model)
        {
            if (ModelState.IsValid)
            {
                var response = MasterManager.AccountManager.Transfer(model, RequestEmail(User));
                if (response.Success)
                {
                    MasterManager.Save();
                    return Ok(new { Response = response });
                }
                return BadRequest(new { Response = response });
            }

            return BadRequest();

        }

        [HttpGet]
        public IActionResult GetAccount([FromQuery] GetAccountVM model)
        {
            if(ModelState.IsValid)
            {
                var account = MasterManager.AccountManager.GetBankAccount(model.Id, RequestEmail(User));
                return Ok(new { Account = account });
            }
            return BadRequest();            
        }

        [HttpGet]
        public IActionResult GetAccountTransactions([FromQuery] GetAccountVM model)
        {
            var transactions = MasterManager.AccountManager.GetAccountTransactions(model.Id, RequestEmail(User));
            return Ok(new { Transactions = transactions });
        }
    }
}
