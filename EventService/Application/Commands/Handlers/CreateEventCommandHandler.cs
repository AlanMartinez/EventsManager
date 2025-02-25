using EventService.Controllers;
using EventService.Infrastructure;
using EventService.Models;
using MediatR;

namespace EventService.Application.Commands.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Event>
    {
        private readonly IGenericRepository<Event> _repository;

        public CreateEventCommandHandler(IGenericRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = new Event { Id = Guid.NewGuid().ToString(), Name = request.Name, Date = request.Date };
            await _repository.AddAsync(newEvent);
            return newEvent;
        }
    }
}
