using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LettercaixaAPI.Services.Implementations
{
    public class FavoriteService: IFavoriteService
    {
        /*private readonly LettercaixaContext _context;
        public FavoriteService(LettercaixaContext context) => _context = context;

        public async Task<ActionResult<Favorite>> CreateFavoriteAsync(string username)
        {
            Profile profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Username.Equals(username));
            Favorite favorite = new Favorite()
            {
                FavoriteId = profile.ProfileId,
                ProfileId = profile.ProfileId,
            };
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return new OkObjectResult(favorite);
        }

        public async Task<ActionResult<Favorite>> AddMovieToFavoritesAsync(string email, int movieId) 
        {
            Favorite favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.Profile.Email.Equals(email));

            foreach (FieldInfo fields in favorite.GetType().GetFields()) 
            {
                if (fields.GetValue(favorite) == null)
                {
                    fields.SetValue(favorite, movieId);
                    break;
                } 
                return new ConflictResult();
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(favorite);
        }

        public async Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId) 
        {
            Favorite favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.Profile.Email.Equals(email));

            foreach (FieldInfo fields in favorite.GetType().GetFields())
            {
                if (fields.GetValue(favorite).Equals(movieId))
                {
                    fields.SetValue(favorite, null);
                    break;
                }
            }
            await _context.SaveChangesAsync();
            return new OkObjectResult(favorite);
        }*/
    }
}
