﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ITHelpDeskApp.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int TicketNum { get; set; }

        [Required(ErrorMessage = "Ticket Title is required")]
        public string TicketTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ticket Description is required")]
        public string TicketDescription { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public string Priority { get; set; } = string.Empty;
        enum Priorities
        {
            Low,
            Medium,
            High
        }

        // Foreign key references
        public int CreatedByUserId { get; set; }
        [ValidateNever]
        public User? CreatedByUser { get; set; } = null;
        public int AssignedToUserId { get; set; }
        [ValidateNever]
        public User? AssignedToUser { get; set; } = null;
        
        
    }
}