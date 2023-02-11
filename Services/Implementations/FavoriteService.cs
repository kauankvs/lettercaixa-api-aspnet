using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LettercaixaAPI.Services.Implementations
{
    public class FavoriteService: IFavoriteService
    {
        private readonly IFavoritesCollectionService _collectionService;
        private readonly LettercaixaContext _context;
        public FavoriteService(IFavoritesCollectionService collectionService, LettercaixaContext context) 
        { 
            _collectionService = collectionService;
            _context = context;
        }

        public async Task<ActionResult> AddMovieToFavoritesAsync(string email, int movieId) 
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            await _collectionService.AddMovieFromDocAsync(profile.ProfileId, movieId);
            return new OkResult();
        }

        public async Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId) 
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            
            return new AcceptedResult();
        }
    }
}
