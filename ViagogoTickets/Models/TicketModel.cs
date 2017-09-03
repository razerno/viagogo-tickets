using GogoKit.Models.Response;
using System.ComponentModel.DataAnnotations;

namespace ViagogoTickets.Models
{
    public class TicketModel
    {
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }
        
        [Display(Name = "Price")]
        public Money Price { get; set; }

        [Display(Name = "Number of Tickets Available")]
        public int? NumAvailable { get; set; }
    }
}
