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
            if (HttpContext.Session.GetString("LoggedInUsername") == null)
            {
                return RedirectToAction("LoginPage");
            }

            // Then, check if logged in user has IsItUser bool set to true
            var loggedInUsername = HttpContext.Session.GetString("LoggedInUsername")?.ToLower();

            var loggedInUser = GetUsers().Where(u => u.Username.ToLower() == loggedInUsername).FirstOrDefault();

            // If yes, then direct user to IT User Summary page
            if (loggedInUser.IsItUser)
            {
                return RedirectToAction("ItUserSummaryPage");
            }

            // If no, then direct user to non-IT User Summary page]
            return RedirectToAction("NonItUserSummaryPage");
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            TempData.Remove("ErrorMessage");
            return View();
        }

        [HttpPost]
        public RedirectToActionResult ValidateLogin(LoginUser loginUser)
        {
            var userWithMatchingUsername = GetUsers().Where(u => u.Username.ToLower().Equals(loginUser.Username.ToLower())).FirstOrDefault();

            if (userWithMatchingUsername == null)
            {
                // Add error message to tempdata that no matching username was found
                TempData["ErrorMessage"] = "No matching username found";
                return RedirectToAction("LoginPage");
            }

            if (!userWithMatchingUsername.Password.Equals(loginUser.Password)) 
            {
                // Add error to tempdata that password is incorrect
                TempData["ErrorMessage"] = "Incorrect password";
                return RedirectToAction("LoginPage");
            }

            // Set the logged in user in tempdata
            HttpContext.Session.SetString("LoggedInUsername", userWithMatchingUsername.Username);
            return RedirectToAction("Index");
        }

        public IActionResult ItUserSummaryPage()
        {
            var tickets = GetTickets();
            return View(tickets);
        }

        public IActionResult NonItUserSummaryPage()
        {
            var tickets = GetTickets();
            return View(tickets);
        }

        private IEnumerable<User> GetUsers()
        {
            return userData.List(new QueryOptions<User> { OrderBy = u => u.UserId });
        }

        private IEnumerable<Ticket> GetTickets()
        {
            return ticketData.List(new QueryOptions<Ticket> { OrderBy = t => t.TicketId, Includes = "AssignedToUser" });
        }
    }
}