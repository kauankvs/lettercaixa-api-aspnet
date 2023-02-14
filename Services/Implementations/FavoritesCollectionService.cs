﻿using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LettercaixaAPI.Services.Implementations
{
    public class FavoritesCollectionService
    {
        private readonly IMongoCollection<Favorite> _favoritesCollection;
        public FavoritesCollectionService(IOptions<LettercaixaDatabaseSettings> lettercaixaSettings) 
        {
            MongoClient client = new MongoClient(lettercaixaSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(lettercaixaSettings.Value.DatabaseName);
            _favoritesCollection = database.GetCollection<Favorite>(lettercaixaSettings.Value.CollectionName);
        }

        public async Task<Favorite?> GetDocAsync(int profileId)
            => await _favoritesCollection.Find(d => d.ProfileId == profileId).FirstOrDefaultAsync();

        public async Task CreateDocAsync(Favorite favorite) 
            => await _favoritesCollection.InsertOneAsync(favorite);

        public async Task UpdateDocAsync(int profileId, Favorite updatedFavorite)
            => await _favoritesCollection.ReplaceOneAsync(d => d.ProfileId == profileId, updatedFavorite);

        public async Task AddMovieFromDocAsync(int profileId, int movieId)
        {
            var addMovie = Builders<Favorite>.Update.Push(d => d.Movies, movieId);
            await _favoritesCollection.FindOneAndUpdateAsync(d => d.ProfileId == profileId, addMovie);
        }

        public async Task RemoveMovieFromDocAsync(int profileId, int movieId)
        {
            var addMovie = Builders<Favorite>.Update.Pull(d => d.Movies, movieId);
            await _favoritesCollection.FindOneAndUpdateAsync(d => d.ProfileId == profileId, addMovie);
        }

        public async Task DeleteDocAsync(int profileId)
            => await _favoritesCollection.DeleteOneAsync(d => d.ProfileId == profileId);

    }
}
