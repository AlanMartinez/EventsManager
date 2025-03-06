using StackExchange.Redis;
using System.Text.Json;

namespace EventService.Services.Impl
{
    public class RedisCacheService<T> : ICacheService<T> where T: class
    {
        private readonly IDatabase _cache;
        private readonly string _cacheKey;

        public RedisCacheService(IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration["Redis:ConnectionString"];
                var redisOptions = ConfigurationOptions.Parse(connectionString);
                redisOptions.ConnectTimeout = 10000;
                redisOptions.SyncTimeout = 10000;
                redisOptions.AbortOnConnectFail = false;

                var redis = ConnectionMultiplexer.Connect(redisOptions);
                _cache = redis.GetDatabase();
                _cacheKey = configuration["Redis:CacheKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a Redis: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<T>?> GetCachedEventsAsync()
        {
            var cachedData = await _cache.StringGetAsync(_cacheKey);
            return cachedData.IsNullOrEmpty ? null : JsonSerializer.Deserialize<IEnumerable<T>>(cachedData);
        }

        public async Task SetCachedEventsAsync(IEnumerable<T> events)
        {
            var serializedData = JsonSerializer.Serialize(events);
            var options = TimeSpan.FromMinutes(10);

            await _cache.StringSetAsync(_cacheKey, serializedData, options);
        }

        public async Task ClearCacheAsync()
        {
            await _cache.KeyDeleteAsync(_cacheKey);
        }
    }
}