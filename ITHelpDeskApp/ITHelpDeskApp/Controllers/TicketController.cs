/*
 * This controls all the logic related to the tickets, such as creating a new one,
 * assigning a ticket, closing a ticket, and displaying the ticket details.
 * 
 */
using ITHelpDeskApp.Models;
using ITHelpDeskApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ITHelpDeskApp.Controllers
{
    public class TicketController : Controller
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
            ViewData["LoggedInFirstName"] = userModel.GetLoggedInUser(LoggedInUsername, AllUsers)?.FirstName;
            return View();
        }

        [HttpPost]
        public RedirectToActionResult SaveNewTicket(Ticket ticket)
        {
            var tickets = ticketData.GetAll();

            // Populate additional Ticket fields
            ticket.TicketNum = ticketModel.CalculateTicketNum(tickets);
            ticket.CreatedBy = userModel.GetLoggedInUserFullName(LoggedInUsername, AllUsers);
            ticket.Status = Ticket.Statuses.Open.ToString();
            ticket.AssignedToName = "Unassigned";
            ticket.CreatedDate = DateTime.Now;

            InsertNewTicket(ticket);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public RedirectToActionResult AssignTicket(int ticketId)
        {
            var ticket = ticketData.Get(ticketId);
            ticket.AssignedToName = userModel.GetLoggedInUserFullName(LoggedInUsername, AllUsers);

            UpdateTicket(ticket);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult TicketSummary(int ticketId)
        {
            var currentUser = userModel.GetLoggedInUser(LoggedInUsername, AllUsers);

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

            UpdateTicket(ticket);

            return RedirectToAction("Index", "Home");
        }

        private string GetLoggedInUsername()
        {
            var loggedInUsername = HttpContext.Session.GetString("LoggedInUsername");

            if (loggedInUsername == null)
                throw new Exception("Cannot find username for logged in user");

            return loggedInUsername;
        }

        private void InsertNewTicket(Ticket ticket)
        {
            ticketData.Insert(ticket);
            ticketData.Save();
        }

        private void UpdateTicket(Ticket ticket)
        {
            ticketData.Update(ticket);
            ticketData.Save();
        }
    }
}
