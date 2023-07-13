#region LibraryReferences
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechBank.Application.Models.API;
using TechBank.Application.Security;
using TechBank.DomainModel;
#endregion

namespace TechBank.Application.Areas.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        #region DependencyInjections
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public SecurityController(JwtSettings jwtSettings, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Register
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register([FromBody] RegisterVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var user = new User
        //            {
        //                UserName = model.Username,
        //                Email = model.Email
        //            };

        //            var result = await userManager.CreateAsync(user, model.Password);

        //            if (result.Succeeded)
        //            {
        //                return Ok("User registered successfully");
        //            }
        //            else
        //            {
        //                return Problem(result.Errors?.FirstOrDefault()?.Description);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        return Problem(detail: "Invalid data");
        //    }
        //}
        #endregion

        #region Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User
                    {
                        UserName = model.Username,
                    };

                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        var currentUser = await _userManager.FindByNameAsync(model.Username);
                        var token = new UserToken();

                        var roles = await _userManager.GetRolesAsync(currentUser);

                        var claims = new List<Claim>();

                        claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

                        token = JwtHelper.GenTokenkey(new UserToken()
                        {
                            EmailId = currentUser.Email,
                            GuidId = currentUser.EntityPublicKey,
                            UserName = currentUser.UserName,
                            Id = Convert.ToUInt16(currentUser.Id),
                        }, _jwtSettings, claims);

                        return Ok(new { message = "Login Successful", token = token });
                    }
                    else
                    {
                        return Problem(detail: "Invalid login attempt", statusCode: 400);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return Problem(detail: "Invalid data");
            }
        }
        #endregion
    }
}
