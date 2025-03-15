using EventService.Application.Commands;
using EventService.Infrastructure;
using EventService.Models;
using MediatR;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IGenericRepository<Event> _repository;

    public DeleteEventCommandHandler(IGenericRepository<Event> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventItem = await _repository.GetByIdAsync(request.Id);
        if (eventItem == null) return false;

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}