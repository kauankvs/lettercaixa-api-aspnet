using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LettercaixaAPI.Services.Implementations
{
    public class FavoriteMoviesService: IFavoriteMoviesService
    {
        private readonly LettercaixaContext _context;
        public FavoriteMoviesService(LettercaixaContext context) => _context = context;

        public async Task<ActionResult<FavoriteMovie>> AddMovieToFavoritesAsync(string email, int movieId) 
        {
            FavoriteMovie favoriteMovie = await _context.FavoriteMovies.FirstOrDefaultAsync(f => f.Profile.Email.Equals(email));

            foreach (FieldInfo fields in favoriteMovie.GetType().GetFields()) 
            {
                if (fields.GetValue(favoriteMovie) == null)
                {
                    fields.SetValue(favoriteMovie, movieId);
                    break;
                }  
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(favoriteMovie);
        }

        public async Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId) 
        {
            FavoriteMovie favoriteMovie = await _context.FavoriteMovies.FirstOrDefaultAsync(f => f.Profile.Email.Equals(email));

            foreach (FieldInfo fields in favoriteMovie.GetType().GetFields())
            {
                if (fields.GetValue(favoriteMovie).Equals(movieId))
                {
                    fields.SetValue(favoriteMovie, null);
                    break;
                }
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(favoriteMovie);
        }
    }
}
