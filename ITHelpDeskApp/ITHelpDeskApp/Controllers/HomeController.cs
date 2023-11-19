using ITHelpDeskApp.Models;
using ITHelpDeskApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITHelpDeskApp.Controllers
{
    public class HomeController : Controller
    {
        // Private variables for storing the different model repositories
        private Repository<User> userData { get; set; }
        private Repository<Ticket> ticketData { get; set; }

        public HomeController(HelpDeskContext ctx)
        {
            userData = new Repository<User>(ctx);
            ticketData = new Repository<Ticket>(ctx);
        }
        public IActionResult Index()
        {
            // Check if Users.Any(IsLoggedInUser == true)
                // Then, check if logged in user has IsItUser bool set to true
                    // If yes, then direct user to IT User Summary page
                    // If no, then direct user to non-IT User Summary page
            return View();
        }
    }
}