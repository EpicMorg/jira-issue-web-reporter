namespace epicmorg.jira.issue.web.reporter.Services.Impl
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;

    public abstract class JsonCache
    {
        private readonly IDistributedCache cache;

        protected JsonCache(IDistributedCache cache)
        {
            this.cache = cache;
        }

        protected async Task<T> GetCachedValue<T>(string key)
        {
            var cache = await this.cache.GetStringAsync(key).ConfigureAwait(false);
            if (cache == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(cache);
        }

        protected Task SetCachedValue<T>(string key, T value, TimeSpan lifetime) =>
            this.cache.SetStringAsync(
                key,
                JsonSerializer.Serialize<T>(value),
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = lifetime,
                });
    }
}
