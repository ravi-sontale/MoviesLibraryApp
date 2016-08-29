using System;
using MoviesLibrary.Interface;
using System.Runtime.Caching;

namespace MoviesLibrary.Provider
{
    /// <summary>
    /// Default Cache provider which implements ICacheProvider
    /// </summary>
    public class DefaultCacheProvider : ICacheProvider
    {
        /// <summary>
        /// Rturns objectCache 
        /// </summary>
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        /// <summary>
        /// Gets data from Cache for the key passed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return Cache[key];
        }

        /// <summary>
        /// Sets data in Cache for the Key passed and sets the cache timeout
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public void Set(string key, object data, int cacheTime)
        {
            var policy = new CacheItemPolicy {AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime)};

            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Check if the Cache is set for given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        /// <summary>
        /// Removes the data for given key from cache
        /// </summary>
        /// <param name="key"></param>
        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }

    }
}
