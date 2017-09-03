using System.Collections.Generic;
using System.Threading.Tasks;
using ViagogoTickets.Models;

namespace ViagogoTickets.Services
{
    public interface IEventService
    {
        Task GetToken();
        Task<EventModel> GetEvent(int eventId);
        Task<IEnumerable<EventModel>> GetAllEvents(int categoryId);
        Task<IEnumerable<TicketModel>> GetAllTicketsByEvent(int eventId);
        HashSet<int> GetLowestPriceEventsByCountry(IEnumerable<EventModel> events);
    }
}
