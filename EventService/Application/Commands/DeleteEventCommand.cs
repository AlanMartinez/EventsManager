using MediatR;

public record DeleteEventCommand(string Id) : IRequest<bool>;
