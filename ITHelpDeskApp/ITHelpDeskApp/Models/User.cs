﻿using ITHelpDeskApp.Models.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ITHelpDeskApp.Models
{
    public class User
    {
        public User() => Tickets = new List<Ticket>();
        
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required")]
        public bool IsItUser { get; set; } = false;
        public bool IsLoggedInuser { get; set; } = false;
        public string Department { get; set; } = string.Empty;
        public enum Departments
        {
            IT,
            Sales,
            Accounting,
            Support,
            Development
        }

        [ValidateNever]
        public ICollection<Ticket> Tickets { get; set; }

        public User GetLoggedInUser(string loggedInUsername, List<User> users)
        {            
            var loggedInUser = users.Where(u => u.Username.Equals(loggedInUsername)).FirstOrDefault();

            if (loggedInUser != null)
                return loggedInUser;

            return null;
        }

        public string GetLoggedInUserFullName(string loggedInUsername, List<User> users)
        {
            var loggedInUser = GetLoggedInUser(loggedInUsername, users);

            var firstName = loggedInUser?.FirstName;
            var lastName = loggedInUser?.LastName;

            return $"{firstName} {lastName}";
        }
    }
}
