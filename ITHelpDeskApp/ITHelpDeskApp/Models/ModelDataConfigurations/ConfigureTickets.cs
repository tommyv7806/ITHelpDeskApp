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
            // An IT User can be assigned to many Tickets
            builder.HasOne<User>(t => t.AssignedToUser)
            .WithMany(g => g.Tickets)
            .HasForeignKey(s => s.AssignedToUserId);

            // A non-IT User can create many Tickets
            builder.HasOne<User>(t => t.CreatedByUser)
            .WithMany(g => g.Tickets)
            .HasForeignKey(s => s.CreatedByUserId);

            builder.HasData(
                // Create 2 closed tickets - 1 assigned to each IT User from each non-IT user
                new Ticket
                {
                    TicketId = 1,
                    TicketNum = 100,
                    TicketTitle = "Desktop won't turn on",
                    TicketDescription = "The light on the tower turns on, but nothing ever shows up on the monitor. The monitor is on. Please help.",
                    Status = Ticket.Statuses.Closed.ToString(),
                    Priority = Ticket.Priorities.Medium.ToString(),
                    CreatedDate = DateTime.Now.AddDays(-33),
                    ClosedDate = DateTime.Now.AddDays(-26).AddHours(3).AddMinutes(37),
                    CreatedByUserId = 4,
                    AssignedToUserId = 1
                },
                new Ticket
                {
                    TicketId = 2,
                    TicketNum = 101,
                    TicketTitle = "Can't connect to internal Accounting server",
                    TicketDescription = "On Friday, I was able to connect to the Accounting server; however, now when I try to connect I receive a 'cannot connect to server' error message.",
                    Status = Ticket.Statuses.Closed.ToString(),
                    Priority = Ticket.Priorities.Low.ToString(),
                    CreatedDate = DateTime.Now.AddDays(-22),
                    ClosedDate = DateTime.Now.AddDays(-20).AddHours(1).AddMinutes(48),
                    CreatedByUserId = 3,
                    AssignedToUserId = 2
                },

                // Create 2 open tickets - 1 assigned to each IT User from each non-IT user
                new Ticket
                {
                    TicketId = 3,
                    TicketNum = 101,
                    TicketTitle = "Can't connect to Internet",
                    TicketDescription = "Receiving a '404 error' on every webpage. This is an urgent request.",
                    Status = Ticket.Statuses.Open.ToString(),
                    Priority = Ticket.Priorities.High.ToString(),
                    CreatedDate = DateTime.Now.AddHours(-2).AddMinutes(-33),
                    CreatedByUserId = 4,
                    AssignedToUserId = 2
                },
                new Ticket
                {
                    TicketId = 4,
                    TicketNum = 101,
                    TicketTitle = "Request for new Quickbooks licenses",
                    TicketDescription = "Hello, this is a request to purchase 2 licenses for Quickbooks Enterprise 2023. These licenses will be for the new hires starting next week.",
                    Status = Ticket.Statuses.Open.ToString(),
                    Priority = Ticket.Priorities.Medium.ToString(),
                    CreatedDate = DateTime.Now.AddDays(-1).AddHours(-3),
                    CreatedByUserId = 3,
                    AssignedToUserId = 1
                }
            );
        }
    }
}
