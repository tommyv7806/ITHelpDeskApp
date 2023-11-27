using ITHelpDeskApp.Models.ModelDataConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ITHelpDeskApp.Models
{
    public class HelpDeskContext : DbContext
    {
        public HelpDeskContext(DbContextOptions<HelpDeskContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null;
        public DbSet<Ticket> Tickets { get; set; } = null;

        // Sets up the initial data in the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigureUsers());
            modelBuilder.ApplyConfiguration(new ConfigureTickets());
        }
    }
}
