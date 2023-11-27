using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using System.Reflection.Emit;

namespace ITHelpDeskApp.Models.ModelDataConfigurations
{
    public class ConfigureTickets : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasData(
                // Create 2 closed tickets - 1 assigned to each IT User from each non-IT user
                new Ticket
                {
                    TicketId = 1,
                    TicketNum = 100,
                    TicketTitle = "Desktop won't turn on",
                    TicketDescription = "The light on the tower turns on, but nothing ever shows up on the monitor. The monitor is on. Please help.",
                    ResolutionSummary = "HDMI cable that connected the monitor to the PC was busted. Replaced it with a new one.",
                    Status = Ticket.Statuses.Closed.ToString(),
                    Priority = Ticket.Priorities.Medium.ToString(),
                    CreatedDate = DateTime.Now.AddDays(-33),
                    ClosedDate = DateTime.Now.AddDays(-26).AddHours(3).AddMinutes(37),
                    CreatedBy = "Alberta Crocodile",
                    AssignedToName = "Sally Smith"
                },
                new Ticket
                {
                    TicketId = 2,
                    TicketNum = 101,
                    TicketTitle = "Can't connect to internal Accounting server",
                    TicketDescription = "On Friday, I was able to connect to the Accounting server; however, now when I try to connect I receive a 'cannot connect to server' error message.",
                    ResolutionSummary = "User was not connected to VPN. Once connected, the issue was resolved.",
                    Status = Ticket.Statuses.Closed.ToString(),
                    Priority = Ticket.Priorities.Low.ToString(),
                    CreatedDate = DateTime.Now.AddDays(-22),
                    ClosedDate = DateTime.Now.AddDays(-20).AddHours(1).AddMinutes(48),
                    CreatedBy = "John Doe",
                    AssignedToName = "Albert Gator"
                },

                // Create 2 open tickets - 1 assigned to each IT User from each non-IT user
                new Ticket
                {
                    TicketId = 3,
                    TicketNum = 102,
                    TicketTitle = "Can't connect to Internet",
                    TicketDescription = "Receiving a '404 error' on every webpage. This is an urgent request.",
                    Status = Ticket.Statuses.Open.ToString(),
                    Priority = Ticket.Priorities.High.ToString(),
                    CreatedDate = DateTime.Now.AddHours(-2).AddMinutes(-33),
                    CreatedBy = "Alberta Crocodile",
                    AssignedToName = "Albert Gator"
                },
                new Ticket
                {
                    TicketId = 4,
                    TicketNum = 103,
                    TicketTitle = "Request for new Quickbooks licenses",
                    TicketDescription = "Hello, this is a request to purchase 2 licenses for Quickbooks Enterprise 2023. These licenses will be for the new hires starting next week.",
                    Status = Ticket.Statuses.Open.ToString(),
                    Priority = Ticket.Priorities.Medium.ToString(),
                    CreatedDate = DateTime.Now.AddDays(-1).AddHours(-3),
                    CreatedBy = "John Doe",
                    AssignedToName = "Sally Smith"
                }
            );
        }
    }
}
