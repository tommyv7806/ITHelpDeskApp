using ITHelpDeskApp.Models;
using ITHelpDeskApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace ITHelpDeskApp.Controllers
{
    public class HomeController : Controller
    {
        // Private variables for storing the different model repositories
        private Repository<User> userData { get; set; }
        private Repository<Ticket> ticketData { get; set; }
        private Ticket ticketModel;
        private User userModel;

        private string _loggedInUsername;
        private string LoggedInUsername
        {
            get { return _loggedInUsername ?? GetLoggedInUsername(); }
            set { _loggedInUsername = value; }
        }

        public HomeController(HelpDeskContext ctx)
        {
            userData = new Repository<User>(ctx);
            ticketData = new Repository<Ticket>(ctx);
            ticketModel = new Ticket();
            userModel = new User();
        }
        public RedirectToActionResult Index()
        {
            // Check if Users.Any(IsLoggedInUser == true)
            if (HttpContext.Session.GetString("LoggedInUsername") == null)
            {
                return RedirectToAction("LoginPage");
            }

            // Then, check if logged in user has IsItUser bool set to true
            var loggedInUser = GetLoggedInUser();

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

        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult ValidateLogin(LoginUser loginUser)
        {
            var userWithMatchingUsername = userData.List(new QueryOptions<User>
                { Where = u => u.Username.ToLower().Equals(loginUser.Username.ToLower()) }).FirstOrDefault();

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
            ViewData["LoggedInFirstName"] = GetLoggedInUser()?.FirstName;

            ViewBag.UnassignedTickets = ticketData.List(new QueryOptions<Ticket> 
                { Where = t => t.AssignedToName.Equals("Unassigned") });

            var tickets = ticketData.GetAll();
            return View(tickets);
        }

        public IActionResult NonItUserSummaryPage()
        {
            ViewData["LoggedInFirstName"] = GetLoggedInUser()?.FirstName;

            // Only want to get the tickets that were created by the logged in non-IT User
            var tickets = ticketData.List(new QueryOptions<Ticket>{ 
                Where = t => t.CreatedBy.ToLower() == GetLoggedInUserFullName().ToLower(),
                OrderBy = t => t.TicketId
            }).ToList();

            return View(tickets);
        }

        private string GetLoggedInUsername()
        {
            var loggedInUsername = HttpContext.Session.GetString("LoggedInUsername");

            if (loggedInUsername == null)
            {
                throw new Exception("Cannot find username for logged in user");
            }

            return loggedInUsername;
        }

        private User GetLoggedInUser()
        {
            return userModel.GetLoggedInUser(LoggedInUsername, userData.GetAll());
        }

        private string GetLoggedInUserFullName()
        {
            return userModel.GetLoggedInUserFullName(LoggedInUsername, userData.GetAll());
        }
    }
}