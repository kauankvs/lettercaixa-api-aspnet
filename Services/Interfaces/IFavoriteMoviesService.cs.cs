using LettercaixaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LettercaixaAPI.Services.Interfaces
{
    public interface IFavoriteMoviesService
    {
        public Task<ActionResult<FavoriteMovie>> AddMovieToFavoritesAsync(string email, int movieId);
        public Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId);
    }
}
