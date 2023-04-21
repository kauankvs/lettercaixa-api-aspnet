using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography.Xml;

namespace LettercaixaAPI.Services.Implementations
{
    public class FavoriteService: IFavoriteService
    {
        private readonly LettercaixaContext _context;
        public FavoriteService(LettercaixaContext context) 
            => _context = context;
        

        public async Task<ActionResult<FavoriteMovie>> AddMovieToFavoritesAsync(string email, int movieId) 
        {
            Profile? profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email);
            if (profile == null)
                return new BadRequestResult();

            FavoriteMovie favorite = new()
            {
                ProfileId = profile.ProfileId,
                MovieId = movieId,
            };
            await _context.FavoriteMovies.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return new OkObjectResult(favorite);
        }

        public async Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId) 
        {
            Profile profile = await _context.Profiles.Include(p => p.FavoriteMovies).FirstOrDefaultAsync(p => p.Email.Equals(email));
            if (profile == null)
                return new BadRequestResult();

            FavoriteMovie? favorite = profile.FavoriteMovies.FirstOrDefault(f => f.MovieId == movieId);
            if (favorite == null)
                return new BadRequestResult();

            _context.FavoriteMovies.Remove(favorite);
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<List<int>>> GetFavoriteMoviesFromProfileAsync(string profileEmail)
        {
            List<int> moviesId = new List<int>();
            Profile profile = await _context.Profiles.Include(p => p.FavoriteMovies).AsNoTracking().FirstOrDefaultAsync(p => p.Email.Equals(profileEmail));
            List<FavoriteMovie> favMovies = profile.FavoriteMovies.ToList();
            foreach(var fav in favMovies)
                moviesId.Add(fav.MovieId);

            return new OkObjectResult(moviesId);
        }

    }
}
