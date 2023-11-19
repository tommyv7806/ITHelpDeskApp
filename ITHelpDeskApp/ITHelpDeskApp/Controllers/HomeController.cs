using ITHelpDeskApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITHelpDeskApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}