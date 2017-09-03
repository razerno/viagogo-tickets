using System.Collections.Generic;

namespace ViagogoTickets.Models
{
    public class TicketsViewModel
    {
        public EventModel Event { get; set; }
        public IEnumerable<TicketModel> TicketList { get; set; }
    }
}
