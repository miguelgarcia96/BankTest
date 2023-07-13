using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBank.Application.Models;
using TechBank.Data;

namespace TechBank.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Dev")]
    public class DevController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new DevIndexVM();            
            
            Database database = DatabaseFactory.CreateDatabase();
            
            model.Server = database.DataSource;
            model.Database = database.InitialCatalog;            
            model.Port = Request.Host.Port.Value.ToString();
            model.Version = "2022.02.09- v0.0.1 (production)";
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Seed()
        {
            Database database = DatabaseFactory.CreateDatabase();
            database.Seed();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DropSchema()
        {
            Database database = DatabaseFactory.CreateDatabase();
            database.DropSchema();
            return RedirectToAction("Index");
        }
    }
}
