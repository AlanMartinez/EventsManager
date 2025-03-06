using AutoMapper;
using EventService.Infrastructure;
using EventService.Models;
using MediatR;

namespace EventService.Application.Commands.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Event>
    {
        private readonly IGenericRepository<Event> _repository;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IGenericRepository<Event> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = _mapper.Map<Event>(request);
            await _repository.AddAsync(newEvent);
            return newEvent;
        }
    }
}
