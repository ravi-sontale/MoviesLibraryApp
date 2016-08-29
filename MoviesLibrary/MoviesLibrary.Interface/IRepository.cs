using System.Collections.Generic;

namespace MoviesLibrary.Interface
{
    /// <summary>
    /// Defines the operations related to cache
    /// </summary>

    public interface IRepository<T>
    {
        void ClearCache();
        int Create(T movie);
        IEnumerable<T> GetAllData();
        T GetDataById(int movieId);
        void Update(T movie);
        IEnumerable<T> SearchMovies(string searchText);
    }
}
