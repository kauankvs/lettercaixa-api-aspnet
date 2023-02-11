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

        public async Task<ActionResult<Favorite>> CreateFavoriteToProfileAsync(string email)
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            Favorite favorite = new Favorite()
            {
                ProfileId = profile.ProfileId
            };
            await _collectionService.CreateDocAsync(favorite);
            return new OkObjectResult(favorite);
        }

        public async Task<ActionResult<Favorite>> AddMovieToFavoritesAsync(string email, int movieId) 
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            await _collectionService.AddMovieFromDocAsync(profile.ProfileId, movieId);
            Favorite favorite = await _collectionService.GetDocAsync(profile.ProfileId);
            return new OkObjectResult(favorite);
        }

        public async Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, int movieId) 
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            await _collectionService.RemoveMovieFromDocAsync(profile.ProfileId, movieId);
            return new AcceptedResult();
        }
    }
}
