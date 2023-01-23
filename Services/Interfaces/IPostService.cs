using LettercaixaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LettercaixaAPI.Services.Interfaces
{
    public interface IPostService
    {
        public Task<ActionResult<Post>> AddCommentaryToMovieAsync(string email, int movieId, string comment, int score);
        public Task<ActionResult> RemoveCommentaryToMovieAsync(string email, int movieId);
    }
}
