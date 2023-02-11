using LettercaixaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LettercaixaAPI.Services.Interfaces
{
    public interface IFavoriteService
    {
        public Task<ActionResult<Favorite>> CreateFavoriteToProfileAsync(string email);
        public Task<ActionResult<Favorite>> AddMovieToFavoritesAsync(string email, int movieId);
        public Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId);
    }
}
