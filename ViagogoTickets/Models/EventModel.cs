using GogoKit.Models.Response;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViagogoTickets.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Venue { get; set; }

        public string Country { get; set; }

        [Display(Name = "From")]
        public DateTimeOffset? StartDate { get; set; }

        [Display(Name = "To")]
        public DateTimeOffset? EndDate { get; set; }

        [Display(Name = "Lowest Price")]
        public Money MinTicketPrice { get; set; }

        public string Restrictions { get; set; }
    }
}
