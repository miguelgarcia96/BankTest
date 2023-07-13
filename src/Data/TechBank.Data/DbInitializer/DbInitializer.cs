using TechBank.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TechBank.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly WebAppContext _webAppContext;

        public DbInitializer(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            WebAppContext webAppContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _webAppContext = webAppContext;
        }

        public void Initialize()
        {
            try
            {
                // Apply migrations if they are not applied
                if (_webAppContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _webAppContext.Database.Migrate();

                    // Create roles if they are not created

                    if (!_roleManager.RoleExistsAsync(UserRole.Admin.ToString()).GetAwaiter().GetResult())
                    {
                        // Create all roles
                        _roleManager.CreateAsync(new Role(UserRole.Dev.ToString())).GetAwaiter().GetResult();
                        _roleManager.CreateAsync(new Role(UserRole.Admin.ToString())).GetAwaiter().GetResult();
                        _roleManager.CreateAsync(new Role(UserRole.Customer.ToString())).GetAwaiter().GetResult();
                    }

                    InitialData();
                }                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void InitialData()
        {
            #region Dev user            
            // Create Dev user
            var email = "miguelgarcia@techbank.com";
            var name = "Miguel";
            var dateNow = DateTime.Now;

            var contact = new Contact
            {
                EntityPublicKey = Guid.NewGuid(),
                FirstName = name,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
            };

            contact.DisplayName = contact.FullName;

            _userManager.CreateAsync(new User
            {
                EntityPublicKey = Guid.NewGuid(),
                Email = email,
                UserName = email,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
                Contact = contact,
            }, "Test1234!").GetAwaiter().GetResult();

            var user = _webAppContext.Users.FirstOrDefault(e => e.Email == email);

            _userManager.AddToRoleAsync(user, UserRole.Dev.ToString()).GetAwaiter().GetResult();

            #endregion

            #region Admin user

            // Create Admin user
            email = "adminone@techbank.com";
            name = "Admin";

            contact = new Contact
            {
                EntityPublicKey = Guid.NewGuid(),
                FirstName = name,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
            };

            contact.DisplayName = contact.FullName;

            _userManager.CreateAsync(new User
            {
                EntityPublicKey = Guid.NewGuid(),
                Email = email,
                UserName = email,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
                Contact = contact,
            }, "Test1234!").GetAwaiter().GetResult();

            var adminUser = _webAppContext.Users.FirstOrDefault(e => e.Email == email);

            _userManager.AddToRoleAsync(adminUser, UserRole.Admin.ToString()).GetAwaiter().GetResult();

            #endregion

            #region CustomerOne

            // Create Admin user
            email = "test1@techbank.com";
            name = "Customer One";

            contact = new Contact
            {
                EntityPublicKey = Guid.NewGuid(),
                FirstName = name,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
            };

            contact.DisplayName = contact.FullName;

            _userManager.CreateAsync(new User
            {
                EntityPublicKey = Guid.NewGuid(),
                Email = email,
                UserName = email,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
                Contact = contact,
            }, "Test1234!").GetAwaiter().GetResult();

            var customerUser = _webAppContext.Users.FirstOrDefault(e => e.Email == email);

            _userManager.AddToRoleAsync(customerUser, UserRole.Customer.ToString()).GetAwaiter().GetResult();

            var customer = new AppCustomer
            {
                EntityPublicKey = Guid.Parse("9B161BA9-E5F5-44BA-9820-4F4C36EE9BDA"),
                User = customerUser,
                Created = dateNow,
                CreatedBy = 1,
                Modified = dateNow,
                ModifiedBy = 1,
            };

            var accounts = new List<BankAccount> {
                new BankAccount {
                    EntityPublicKey = Guid.Parse("80643B2B-D5B2-45A0-8375-A36323057E5B"),
                    BankAccountTypeId = (int)BankAccountType.Check,
                    Alias = "Default checks",
                    Balance = 500,                    
                    Created = dateNow,
                    CreatedBy = 1,
                    Modified = dateNow,
                    ModifiedBy = 1,
                },
                new BankAccount {
                    EntityPublicKey = Guid.Parse("E2F85739-9240-4F8F-986C-CBD6F7E92CCE"),
                    BankAccountTypeId = (int)BankAccountType.Savings,
                    Alias = "Savings",
                    Balance = 100,
                    Created = dateNow,
                    CreatedBy = 1,
                    Modified = dateNow,
                    ModifiedBy = 1,
                }
            };

            customer.Accounts = accounts;

            _webAppContext.Customers.Add(customer);

            _webAppContext.SaveChanges();
            #endregion
        }
    }
}
