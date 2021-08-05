using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required()]
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        [Required()]
        [DisplayName("Start Time")]

        public DateTime StartTime { get; set; }
        [Required()]
        [DisplayName("End Time")]
        [Remote("IsEndTimeValid", "Event", AdditionalFields = "StartTime",ErrorMessage ="End Time must be greater than Start Time")]

        public DateTime EndTime { get; set; }
        [Required()]

        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayName("Event Type")]
        [Required()]
        public EventType EventType { get; set; }
        public string CreatedBy { get; set; }
        [ForeignKey("Service")]
        [Required()]
        public int ServiceId { get; set; }

        public Service Services { get; set; }
        [ForeignKey("Customer")]
        [Required()]
        public int CustomerId { get; set; }
        public Customer Customers { get; set; } 
    }
    public enum EventType
    {
        Internal,External
    }
}
