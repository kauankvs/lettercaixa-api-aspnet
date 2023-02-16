using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LettercaixaAPI.Services.Interfaces
{
    public interface IPostService
    {
        public Task<ActionResult<Post>> AddCommentaryToMovieAsync(string email, PostDTO postInput);
        public Task<ActionResult> RemoveCommentaryToMovieAsync(string email, int movieId);
    }
}
