using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ViagogoTickets.Models;
using ViagogoTickets.Services;

namespace ViagogoTickets.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IEventService _service;

        public HomeController(IConfiguration config, IEventService service)
        {
            _config = config;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            await _service.GetToken();
            return RedirectToAction("Events");
        }

        public async Task<IActionResult> Events()
        {
            IEnumerable<EventModel> events = await _service.GetAllEvents(Convert.ToInt32(_config["CATEGORY_ID"]));
            HashSet<int> cheapestEvents = _service.GetLowestPriceEventsByCountry(events);

            var model = new EventsViewModel
            {
                EventList = events,
                CheapestEventIds = cheapestEvents
            };

            return View(model);
        }

        public async Task<IActionResult> Tickets(int eventId)
        {
            var model = new TicketsViewModel
            {
                Event = await _service.GetEvent(eventId),
                TicketList = await _service.GetAllTicketsByEvent(eventId)
            };
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
