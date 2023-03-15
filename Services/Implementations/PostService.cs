using LettercaixaAPI.DTOs;
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

        public async Task<ActionResult<Post>> AddCommentaryToMovieAsync(string email, PostDTO postInput) 
        {
            Profile profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Email.Equals(email));
            Post post = new Post()
            {
                ProfileId = profile.ProfileId,
                MovieId = postInput.MovieId,
                Comment = postInput.Comment,
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

        public async Task<ActionResult<List<PostDisplay>>> GetMovieComments(int movieId) 
        {
            List<Post>? comments = await _context.Posts.AsNoTracking().Where(p => p.MovieId == movieId).ToListAsync();
            if (comments == null)
                return new NoContentResult();

            List<PostDisplay> commentsDisplay = TransformPostsForDisplayAsync(comments).Result;
            return new OkObjectResult(commentsDisplay);
        }

        public async Task<List<PostDisplay>> TransformPostsForDisplayAsync(List<Post> posts)
        {
            List<PostDisplay> postsDisplay = new List<PostDisplay>();
            foreach (Post post in posts)
            {
                Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.ProfileId == post.ProfileId);
                postsDisplay.Add(new PostDisplay
                {
                    ProfileId = post.ProfileId,
                    MovieId = post.MovieId,
                    Comment = post.Comment,
                    Username = profile.Username,
                    FullName = profile.FirstName + " " + profile.LastName,
                    ProfilePicture = profile.ProfilePicture,
                });
            }
            return postsDisplay;
        }
        
      
    }
}
