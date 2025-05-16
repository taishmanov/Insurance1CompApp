using Insurance1CompAppServices.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Insurance1CompAppServices
{
    public class CountryGwpService
    {
        private readonly ICountryGwpRepository _repository;
        private readonly IMemoryCache _cache; // could be a IDistributedCache or custom abstract cache with adaptors for IDistributedCache and IMemoryCache

        public CountryGwpService(ICountryGwpRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<Dictionary<string, double>> GetAverageGwpAsync(string country, List<string> lobs)
        {
            var cacheKey = $"avgGwp:{country}:{string.Join(",", lobs.OrderBy(l => l))}";

            if (!_cache.TryGetValue(cacheKey, out Dictionary<string, double> cachedResult))
            {
                var data = await _repository.GetGwpDataAsync(country, lobs, 2008, 2015); // hard coded year range. Could be comen from user request

                var result = data.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Average()
                );

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30)); // could be taken from the appconfig

                _cache.Set(cacheKey, result, cacheEntryOptions);

                return result;
            }

            return cachedResult;
        }
    }
}
