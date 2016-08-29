using System.Collections.Generic;

namespace MoviesLibrary.Interface
{
    /// <summary>
    /// Defines the operations that can be performed on Cache
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Gets the object cached from cache for given key
        /// </summary>
        /// <param name="key">A <see cref="string"/> uniquely identifies the cached data</param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// Sets the object data in Cache for given key and expiration time
        /// </summary>
        /// <param name="key">A <see cref="string"/> uniquely identifies the cached data</param>
        /// <param name="data">A <see cref="object"/> to be cached</param>
        /// <param name="cacheTime">A <see cref="int"/> time interaval for cache expiration</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Checks if the Cache is set for given key
        /// </summary>
        /// <param name="key">A <see cref="string"/> uniquely identifies the cached data</param>
        /// <returns>boolean</returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the Object data from Cache for given key
        /// </summary>
        /// <param name="key">A <see cref="string"/> uniquely identifies the cached data</param>
        void Invalidate(string key);
    }
}
