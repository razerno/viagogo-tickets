using GogoKit;
using GogoKit.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViagogoTickets.Models;

namespace ViagogoTickets.Services
{
    public class ViagogoEventService : IEventService
    {
        private readonly IViagogoClient _client;

        public ViagogoEventService(IViagogoClient client)
        {
            _client = client;
        }

        public async Task GetToken()
        {
            var token = await _client.OAuth2.GetClientAccessTokenAsync(new string[] { });
            await _client.TokenStore.SetTokenAsync(token);
        }

        public async Task<EventModel> GetEvent(int eventId)
        {
            Event e = await _client.Events.GetAsync(eventId);
            return ConvertEventToModel(e);
        }

        public async Task<IEnumerable<EventModel>> GetAllEvents(int categoryId)
        {
            IReadOnlyList<Event> events = await _client.Events.GetAllByCategoryAsync(categoryId);

            return events.Select(e => ConvertEventToModel(e)).ToList();
        }

        public async Task<IEnumerable<TicketModel>> GetAllTicketsByEvent(int eventId)
        {
            IReadOnlyList<Listing> listings = await _client.Listings.GetAllByEventAsync(eventId);

            return listings.Select(l => ConvertListingToModel(l)).ToList();
        }

        private EventModel ConvertEventToModel(Event e)
        {
            return new EventModel()
            {
                Id = e.Id ?? -1,
                Name = e.Name,
                Venue = e.Venue.Name + ", " + e.Venue.City,
                Country = e.Venue.Country.Name,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                MinTicketPrice = e.MinTicketPrice,
                Restrictions = e.Restrictions,
            };
        }

        private TicketModel ConvertListingToModel(Listing l)
        {
            return new TicketModel()
            {
                Id = l.Id ?? -1,
                Type = l.TicketType.Name,
                Price = l.TicketPrice,
                NumAvailable = l.NumberOfTickets,
            };
        }

        public struct EventPrice
        {
            public int id;
            public decimal price;
        }

        public HashSet<int> GetLowestPriceEventsByCountry(IEnumerable<EventModel> events)
        {
            Dictionary<string, EventPrice> map = new Dictionary<string, EventPrice>();

            foreach(EventModel e in events)
            {
                if (map.TryGetValue(e.Country, out EventPrice oldPrice))
                {
                    if (oldPrice.price > e.MinTicketPrice.Amount)
                    {
                        map[e.Country] = new EventPrice { id = e.Id, price = (decimal)e.MinTicketPrice.Amount };
                    }
                }
                else
                {
                    if (e.MinTicketPrice.Amount != null)
                    {
                        map[e.Country] = new EventPrice { id = e.Id, price = (decimal)e.MinTicketPrice.Amount };
                    }
                }
            }

            HashSet<int> cheapestEventIds = new HashSet<int>();
            foreach(EventPrice e in map.Values)
            {
                cheapestEventIds.Add(e.id);
            }
            return cheapestEventIds;
        }
    }
}
