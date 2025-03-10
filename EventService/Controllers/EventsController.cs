using EventService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _mediator.Send(new GetAllEventsQuery());
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            var createdEvent = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateEvent), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(string id)
        {
            var eventItem = await _mediator.Send(new GetEventByIdQuery(id));
            if (eventItem == null) return NotFound();
            return Ok(eventItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventCommand command)
        {
            var updated = await _mediator.Send(command);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var deleted = await _mediator.Send(new DeleteEventCommand(id));
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("attendees-rating")]
        public async Task<IActionResult> GetEventAttendeesAndRatingQuery()
        {
            var eventItem = await _mediator.Send(new GetEventAttendeesAndRatingQuery());
            if (eventItem == null) return NotFound();
            return Ok(eventItem);
        }
    }
}
