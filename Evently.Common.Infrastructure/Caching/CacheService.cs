using Evently.Common.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Evently.Common.Infrastructure.Caching
{
    internal class CacheService(IDistributedCache cache) : ICacheService
    {
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            byte[]? result = await cache.GetAsync(key, cancellationToken);
            return result is null ? default : JsonSerializer.Deserialize<T>(result);
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            return cache.RemoveAsync(key, cancellationToken);
        }

        public Task SetAsync<T>(string key,
            T value, 
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default)
        {
            byte[] byteArray = Serialize<T>(value);
            return cache.SetAsync(key, byteArray, CacheOption.Create(expiration), cancellationToken);
        }
        private static byte[] Serialize<T>(T value)
        {
            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);
            JsonSerializer.Serialize(writer, value);
            return buffer.WrittenSpan.ToArray();
        }
    }
}
