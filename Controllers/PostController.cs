using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LettercaixaAPI.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        public PostController(IPostService service) => _service = service;

        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<ActionResult<Post>> AddCommentaryToMovieAsync(int movieId, string comment, int score)
            => await _service.AddCommentaryToMovieAsync(User.FindFirstValue(ClaimTypes.Email), movieId, comment, score);

        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult> RemoveCommentaryToMovieAsync([FromQuery] int movieId)
            => await _service.RemoveCommentaryToMovieAsync(User.FindFirstValue(ClaimTypes.Email), movieId);
    }
}
