using EventService.Application.Queries;
using EventService.Infrastructure;
using EventService.Models;
using MediatR;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Event?>
{
    private readonly IGenericRepository<Event> _repository;

    public GetEventByIdQueryHandler(IGenericRepository<Event> repository)
    {
        _repository = repository;
    }

    public async Task<Event?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}