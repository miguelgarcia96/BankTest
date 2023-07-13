using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechBank.Application.Models;

namespace TechBank.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Dev")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
