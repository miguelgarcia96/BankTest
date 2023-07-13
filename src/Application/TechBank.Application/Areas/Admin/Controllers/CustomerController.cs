using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechBank.Application.Controllers;
using TechBank.Data;
using TechBank.DomainModel;
using TechBank.DomainModel.Models.Admin;

namespace TechBank.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Dev")]
    public class CustomerController : BaseControllerMVC
    {
        #region DI
        private readonly IUserStore<User> _userStore;
        private readonly UserManager<User> _userManager;
        private readonly IUserEmailStore<User> _emailStore;

        public CustomerController(IUnitOfWork unitOfWork, IUserStore<User> userStore, UserManager<User> userManager) : base(unitOfWork)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();            
        }

        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<AppCustomer> customers = MasterManager.CustomerManager.GetAll(true, true);
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CustomerCreateVM();
            return View(model);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task <IActionResult> Create(CustomerCreateVM model)
        {
            if (ModelState.IsValid)
            {                
                var user = MasterManager.CustomerManager.Create(model);
                await _userStore.SetUserNameAsync(user, model.Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRole.Customer.ToString());
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var customer = MasterManager.CustomerManager.GetByPublicKey(id, true);

            if (customer == null)
                return NotFound();

            var model = new CustomerEditVM(customer);

            return View(model);
        }

        private IUserEmailStore<User> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }
    }
}
