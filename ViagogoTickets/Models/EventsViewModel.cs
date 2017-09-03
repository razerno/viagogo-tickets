using System.Collections.Generic;

namespace ViagogoTickets.Models
{
    public class EventsViewModel
    {
        public IEnumerable<EventModel> EventList { get; set; }
        public HashSet<int> CheapestEventIds { get; set; }
    }
}
