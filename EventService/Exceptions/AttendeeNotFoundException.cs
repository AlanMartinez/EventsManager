namespace EventService.Exceptions
{
    public class AttendeeNotFoundException : Exception
    {
        public AttendeeNotFoundException(string? message) : base(message)
        {
        }
    }
}