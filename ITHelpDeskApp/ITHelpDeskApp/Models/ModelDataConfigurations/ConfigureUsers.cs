using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHelpDeskApp.Models.ModelDataConfigurations
{
    public class ConfigureUsers : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                // Setup 2 IT Users
                new User
                {
                    UserId = 1,
                    Username = "ItUser1",
                    Password = "ItPassword1",
                    FirstName = "Sally",
                    LastName = "Smith",
                    Department = User.Departments.IT.ToString(),
                    IsItUser = true,
                },
                new User
                {
                    UserId = 2,
                    Username = "ItUser2",
                    Password = "ItPassword2",
                    FirstName = "Albert",
                    LastName = "Gator",
                    Department = User.Departments.IT.ToString(),
                    IsItUser = true
                },

                // Setup 2 non-IT users
                new User
                {
                    UserId = 3,
                    Username = "NonItUser1",
                    Password = "NonItPassword1",
                    FirstName = "John",
                    LastName = "Doe",
                    Department = User.Departments.Accounting.ToString()
                },
                new User
                {
                    UserId = 4,
                    Username = "NonItUser2",
                    Password = "NonItPassword2",
                    FirstName = "Alberta",
                    LastName = "Crocodile",
                    Department = User.Departments.Support.ToString()
                }
            );
        }
    }
}
