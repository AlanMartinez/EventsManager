namespace EventService.Exceptions
{
    public class EventNotActiveException : Exception
    {
        public EventNotActiveException(string? message) : base(message)
        {
        }
    }
}