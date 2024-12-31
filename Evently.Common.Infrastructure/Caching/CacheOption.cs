using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Infrastructure.Caching
{
    internal static class CacheOption
    {
        public static DistributedCacheEntryOptions DefaultExpiration => new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        };
        public static DistributedCacheEntryOptions Create(TimeSpan? expiration = null) =>
            expiration is not null ? new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            } : DefaultExpiration;
    }
}
