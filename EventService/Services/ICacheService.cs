namespace EventService.Services
{
    public interface ICacheService<T> where T: class
    {
        Task<IEnumerable<T>?> GetCachedEventsAsync();
        Task SetCachedEventsAsync(IEnumerable<T> events);
        Task ClearCacheAsync();
    }
}