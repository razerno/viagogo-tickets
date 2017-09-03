using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViagogoTickets.Models;
using ViagogoTickets.Services;

namespace ViagogoTickets.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IEventService _service;

        public EventsController(IConfiguration config, IEventService service)
        {
            _config = config;
            _service = service;
        }
        
        // GET: api/events
        [HttpGet]
        public async Task<IEnumerable<EventModel>> GetAllEvents()
        {
            IEnumerable<EventModel> events = await _service.GetAllEvents(Convert.ToInt32(_config["CATEGORY_ID"]));
            return events;
        }

        // GET: api/events/{id}/tickets
        [HttpGet("{id}/tickets")]
        public async Task<IEnumerable<TicketModel>> GetTicketsForEvent(int id)
        {
            IEnumerable<TicketModel> tickets = await _service.GetAllTicketsByEvent(id);
            return tickets;
        }
    }
}
