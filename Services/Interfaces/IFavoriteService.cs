using LettercaixaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LettercaixaAPI.Services.Interfaces
{
    public interface IFavoriteService
    {
        public Task<ActionResult> AddMovieToFavoritesAsync(string email, int movieId);
        public Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId);
    }
}
