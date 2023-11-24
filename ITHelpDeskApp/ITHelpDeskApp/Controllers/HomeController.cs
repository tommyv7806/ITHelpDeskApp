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
        private Ticket ticketModel;

        public HomeController(HelpDeskContext ctx)
        {
            userData = new Repository<User>(ctx);
            ticketData = new Repository<Ticket>(ctx);
            ticketModel = new Ticket();
        }
        public IActionResult Index()
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
            ViewData["LoggedInFirstName"] = GetLoggedInUser()?.FirstName;
            ViewBag.IsItUser = true;
            ViewBag.UnassignedTickets = ticketData.List(new QueryOptions<Ticket> 
                { Where = t => t.AssignedToName.Equals("Unassigned") });

            var tickets = GetTickets();
            return View(tickets);
        }

        public IActionResult NonItUserSummaryPage()
        {
            ViewData["LoggedInFirstName"] = GetLoggedInUser()?.FirstName;
            ViewBag.IsItUser = false;

            // Only want to get the tickets that were created by the logged in non-IT User
            var tickets = ticketData.List(new QueryOptions<Ticket>{ 
                Where = t => t.CreatedBy.ToLower() == GetLoggedInUserFullName().ToLower(),
                OrderBy = t => t.TicketId
            }).ToList();

            return View(tickets);
        }

        [HttpGet]
        public IActionResult CreateNewTicket()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult SaveNewTicket(Ticket ticket)
        {
            var tickets = GetTickets().ToList();
            ticket.TicketNum = ticketModel.CalculateTicketNum(tickets);

            ticket.CreatedBy = GetLoggedInUserFullName();
            ticket.Status = Ticket.Statuses.Open.ToString();
            ticket.AssignedToName = "Unassigned";
            ticket.CreatedDate = DateTime.Now;
            
            ticketData.Insert(ticket);
            ticketData.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public RedirectToActionResult AssignTicket(int ticketId)
        {
            var ticket = ticketData.Get(ticketId);

            ticket.AssignedToName = GetLoggedInUserFullName();

            ticketData.Update(ticket);
            ticketData.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult TicketSummary(int ticketId)
        {
            var ticket = ticketData.Get(ticketId);
            var currentUser = GetLoggedInUser();

            if (currentUser.IsItUser)
            {
                ViewBag.IsItUser = true;
                return View(ticket);
            }

            ViewBag.IsItUser = false;

            return View(ticket);
        }

        private IEnumerable<User> GetUsers()
        {
            return userData.List(new QueryOptions<User> { OrderBy = u => u.UserId });
        }

        private IEnumerable<Ticket> GetTickets()
        {
            return ticketData.List(new QueryOptions<Ticket> { OrderBy = t => t.TicketId });
        }

        private User GetLoggedInUser()
        {
            var loggedInUsername = HttpContext.Session.GetString("LoggedInUsername")?.ToLower();

            var loggedInUser = GetUsers().Where(u => u.Username.ToLower() == loggedInUsername).FirstOrDefault();
            if (loggedInUser != null) 
                return loggedInUser;

            return null;
        }

        private string GetLoggedInUserFullName()
        {
            var firstName = GetLoggedInUser()?.FirstName;
            var lastName = GetLoggedInUser()?.LastName;
            return $"{firstName} {lastName}";
        }
    }
}