using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LettercaixaAPI.Services.Implementations
{
    public class PostService: IPostService
    {
        private readonly LettercaixaContext _context;
        public PostService(LettercaixaContext context) => _context = context;

        public async Task<ActionResult<Post>> AddCommentaryToMovieAsync(string email, int movieId, string comment, int score) 
        {
            Profile profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Email.Equals(email));
            Post post = new Post()
            {
                ProfileId = profile.ProfileId,
                MovieId = movieId,
                Comment = comment,
                Score = score,
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return new OkObjectResult(post);
        }

        public async Task<ActionResult> RemoveCommentaryToMovieAsync(string email, int movieId) 
        { 
            Post? post = await _context.Posts.FirstOrDefaultAsync(p => p.Profile.Email == email && p.MovieId == movieId);
            if (post == null)
                return new BadRequestResult();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }
    }
}
