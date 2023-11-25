using ITHelpDeskApp.Models;
using ITHelpDeskApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITHelpDeskApp.Controllers
{
    public class HomeController : Controller
    {
        private Repository<User> userData { get; set; }
        private Repository<Ticket> ticketData { get; set; }
        private Ticket ticketModel;
        private User userModel;

        private string? _loggedInUsername;
        private string LoggedInUsername
        {
            get { return _loggedInUsername ?? GetLoggedInUsername(); }
            set { _loggedInUsername = value; }
        }

        private List<User>? _allUsers;
        private List<User> AllUsers
        {
            get { return _allUsers ?? userData.GetAll(); }
            set { _allUsers = value; }
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
            if (UserIsNotLoggedIn())
                return RedirectToAction("LoginPage", "Login");

            if (LoggedInUserIsItUser())
                return RedirectToAction("ItUserSummaryPage");

            return RedirectToAction("NonItUserSummaryPage");
        }

        public IActionResult ItUserSummaryPage()
        {
            ViewData["LoggedInFirstName"] = userModel.GetLoggedInUser(LoggedInUsername, AllUsers)?.FirstName;

            ViewBag.UnassignedTickets = ticketData.List(new QueryOptions<Ticket> 
                { Where = t => t.AssignedToName.Equals("Unassigned") });

            var tickets = ticketData.GetAll();
            return View(tickets);
        }

        public IActionResult NonItUserSummaryPage()
        {
            ViewData["LoggedInFirstName"] = userModel.GetLoggedInUser(LoggedInUsername, AllUsers)?.FirstName;

            // Only want to get the tickets that were created by the logged in non-IT User
            var tickets = ticketData.List(new QueryOptions<Ticket>{ 
                Where = t => t.CreatedBy == userModel.GetLoggedInUserFullName(LoggedInUsername, AllUsers),
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

        private bool UserIsNotLoggedIn()
        {
            return HttpContext.Session.GetString("LoggedInUsername") == null;
        }

        private bool LoggedInUserIsItUser()
        {
            var loggedInUser = userModel.GetLoggedInUser(LoggedInUsername, AllUsers);

            return loggedInUser.IsItUser;
        }
    }
}