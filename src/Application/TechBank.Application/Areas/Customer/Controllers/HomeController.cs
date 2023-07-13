using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechBank.Application.Models;

namespace TechBank.Application.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        #region HandleEror
        [Route("/Home/HandleError/{code:int}")]
        [HttpGet]
        public IActionResult HandleError(int code)
        {
            switch (code)
            {
                case 404:
                    return RedirectToAction("NotFound");
                default:
                    ViewData["ErrorCode"] = code;

                    ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";

                    return View("~/Views/Shared/HandleError.cshtml");
            }
        }

        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
