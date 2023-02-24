using LettercaixaAPI.Models;
using MongoDB.Driver;

namespace LettercaixaAPI.Services.Interfaces
{
    public interface IFavoritesCollectionService
    {
        public Task<Favorite?> GetDocAsync(int profileId);
        public Task CreateDocAsync(Favorite favorite);
        public Task UpdateDocAsync(int profileId, Favorite updatedFavorite);
        public Task AddMovieFromDocAsync(int profileId, Movie movie);
        public Task RemoveMovieFromDocAsync(int profileId, Movie movie);
        public Task DeleteDocAsync(int profileId);
    }
}

