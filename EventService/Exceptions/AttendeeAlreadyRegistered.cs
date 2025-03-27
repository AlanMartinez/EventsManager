namespace EventService.Exceptions
{
    public class AttendeeAlreadyRegistered : Exception
    {
        public AttendeeAlreadyRegistered(string? message) : base(message)
        {
        }
    }
}
