using EventService.Infrastructure;
using EventService.Models;
using MediatR;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
{
    private readonly IGenericRepository<Event> _repository;

    public UpdateEventCommandHandler(IGenericRepository<Event> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventItem = await _repository.GetByIdAsync(request.Id);
        if (eventItem == null) return false;

        eventItem.Title = request.Name;
        eventItem.Description = request.Date;
        await _repository.UpdateAsync(eventItem);

        return true;
    }
}
