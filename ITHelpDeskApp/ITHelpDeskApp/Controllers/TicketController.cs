using ITHelpDeskApp.Models;
using ITHelpDeskApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITHelpDeskApp.Controllers
{
    public class TicketController : Controller
    {
        // Private variables for storing the different model repositories
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

        public TicketController(HelpDeskContext ctx)
        {
            userData = new Repository<User>(ctx);
            ticketData = new Repository<Ticket>(ctx);
            ticketModel = new Ticket();
            userModel = new User();
        }

        [HttpGet]
        public IActionResult CreateNewTicket()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult SaveNewTicket(Ticket ticket)
        {
            var tickets = ticketData.GetAll();
            ticket.TicketNum = ticketModel.CalculateTicketNum(tickets);

            ticket.CreatedBy = userModel.GetLoggedInUserFullName(LoggedInUsername, userData.GetAll());
            ticket.Status = Ticket.Statuses.Open.ToString();
            ticket.AssignedToName = "Unassigned";
            ticket.CreatedDate = DateTime.Now;

            ticketData.Insert(ticket);
            ticketData.Save();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public RedirectToActionResult AssignTicket(int ticketId)
        {
            var ticket = ticketData.Get(ticketId);

            ticket.AssignedToName = userModel.GetLoggedInUserFullName(LoggedInUsername, userData.GetAll());

            ticketData.Update(ticket);
            ticketData.Save();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult TicketSummary(int ticketId)
        {
            var currentUser = userModel.GetLoggedInUser(LoggedInUsername, userData.GetAll());

            ViewData["LoggedInFirstName"] = currentUser.FirstName;

            var ticket = ticketData.Get(ticketId);
            
            if (currentUser.IsItUser)
            {
                ViewBag.IsItUser = true;
                return View(ticket);
            }

            ViewBag.IsItUser = false;
            return View(ticket);
        }

        [HttpPost]
        public RedirectToActionResult CloseTicket(Ticket ticket)
        {
            ticket.ClosedDate = DateTime.Now;
            ticket.Status = Ticket.Statuses.Closed.ToString();

            ticketData.Update(ticket);
            ticketData.Save();

            return RedirectToAction("Index", "Home");
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
    }
}
