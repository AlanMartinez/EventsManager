using AutoMapper;
using EventService.Infrastructure;
using EventService.Models;
using EventService.Services;
using MediatR;

namespace EventService.Application.Commands.Handlers
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Event>
    {
        private readonly IGenericRepository<Event> _repository;
        private readonly IMapper _mapper;
        private readonly IAzureFunctionService _azureFunctionService;

        public CreateEventCommandHandler(IGenericRepository<Event> repository, IMapper mapper, IAzureFunctionService azureFunctionService)
        {
            _repository = repository;
            _mapper = mapper;
            _azureFunctionService = azureFunctionService;
        }

        public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = _mapper.Map<Event>(request);
            await _repository.AddAsync(newEvent);

            await _azureFunctionService.CallEventCreatedFunctionAsync(newEvent);

            return newEvent;
        }
    }
}
