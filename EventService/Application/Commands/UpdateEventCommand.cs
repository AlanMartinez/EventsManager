using MediatR;

public record UpdateEventCommand(string Id, string Name, string Date) : IRequest<bool>;
