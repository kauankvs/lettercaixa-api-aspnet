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

        public async Task<Favorite> CreateFavoriteToProfileAsync(string email)
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            Favorite favorite = new Favorite()
            {
                ProfileId = profile.ProfileId,
                Movies = new List<Movie>(),
            };
            await _collectionService.CreateDocAsync(favorite);
            return favorite;
        }

        public async Task<ActionResult<Favorite>> AddMovieToFavoritesAsync(string email, Movie movie) 
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            await _collectionService.AddMovieFromDocAsync(profile.ProfileId, movie);
            Favorite favorite = await _collectionService.GetDocAsync(profile.ProfileId);
            return new OkObjectResult(favorite);
        }

        public async Task<ActionResult> RemoveMovieFromFavoritesAsync(string email, Movie movie) 
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            await _collectionService.RemoveMovieFromDocAsync(profile.ProfileId, movie);
            return new AcceptedResult();
        }

        public async Task<ActionResult<Favorite>> GetFavoriteMoviesFromProfileAsync(string profileEmail)
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(profileEmail));
            Favorite profileFavoriteMovies = await _collectionService.GetDocAsync(profile.ProfileId);
            return new OkObjectResult(profileFavoriteMovies);
        }

        public async Task DeleteDocAsync(int profileId)
            => await _collectionService.DeleteDocAsync(profileId);
    }
}
