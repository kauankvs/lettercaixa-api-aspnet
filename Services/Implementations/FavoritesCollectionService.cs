using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LettercaixaAPI.Services.Implementations
{
    public class FavoritesCollectionService: IFavoritesCollectionService
    {
        private readonly IMongoCollection<Favorite> _favoritesCollection;
        public FavoritesCollectionService(IOptions<LettercaixaDatabaseSettings> lettercaixaSettings) 
        {
            var mongoUrl = new MongoUrl(Settings.MongoUrl);
            var settings = MongoClientSettings.FromUrl(mongoUrl);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("Lettercaixa");
            _favoritesCollection = database.GetCollection<Favorite>("Favorites");
        }

        public async Task<Favorite?> GetDocAsync(int profileId)
            => await _favoritesCollection.Find(d => d.ProfileId == profileId).FirstOrDefaultAsync();

        public async Task CreateDocAsync(Favorite favorite) 
            => await _favoritesCollection.InsertOneAsync(favorite);

        public async Task UpdateDocAsync(int profileId, Favorite updatedFavorite)
            => await _favoritesCollection.FindOneAndReplaceAsync(d => d.ProfileId == profileId, updatedFavorite);

        public async Task AddMovieFromDocAsync(int profileId, Movie movie)
        {
            var filter = Builders<Favorite>.Filter.Eq(f => f.ProfileId, profileId);
            var update = Builders<Favorite>.Update.Push(f => f.Movies, movie);
            await _favoritesCollection.UpdateOneAsync(filter, update);   
        }

        public async Task RemoveMovieFromDocAsync(int profileId, Movie movie)
        {
            var filter = Builders<Favorite>.Filter.Eq(f => f.ProfileId, profileId);
            var update = Builders<Favorite>.Update.Pull(f => f.Movies, movie);
            await _favoritesCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteDocAsync(int profileId)
            => await _favoritesCollection.DeleteOneAsync(d => d.ProfileId == profileId);

    }
}
