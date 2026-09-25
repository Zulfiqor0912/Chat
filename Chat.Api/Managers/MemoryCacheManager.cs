using Microsoft.Extensions.Caching.Memory;

namespace Chat.Api.Managers;

public class MemoryCacheManager(IMemoryCache memoryCache)
{
    public void AddOrUpdateDtos(string key, object dtos)
    {
        memoryCache.Set(key, dtos);
    }

    public object? GetDtos(string key)
    {
        if (memoryCache.TryGetValue(key, out object? value))
            return value;
        return null;
    }
}