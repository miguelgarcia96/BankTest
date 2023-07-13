#region LibraryReferences
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBank.Application.Controllers;
using TechBank.Application.Models.API.Admin;
using TechBank.Data;

#endregion

namespace TechBank.Application.Areas.API.Admin
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class CustomerController : BaseControllerAPI
    {
        #region DI        
        public CustomerController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        [HttpGet]
        public IActionResult GetBankAccounts([FromQuery] GetCustomerAccountsVM model)
        {
            if(ModelState.IsValid)
            {
                var customerAccounts = MasterManager.AccountManager.GetCustomerAccounts(model.Email);
                return Ok(new { Accounts = customerAccounts });
            }
            
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateBankAccount([FromBody] CreateBankAccountVM? model)
        {
            if (model != null && ModelState.IsValid)
            {
                var bankAccount = model.GetBankAccount();
                MasterManager.AccountManager.CreateBankAccount(bankAccount, model.Email);
                MasterManager.AuthSave(RequestEmail(User));
                return Ok();
            }

            return BadRequest();
        }
    }
}
