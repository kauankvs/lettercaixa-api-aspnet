using LettercaixaAPI.DTOs;
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
        public async Task<ActionResult<Post>> AddCommentaryToMovieAsync([FromBody] PostDTO postInput) 
            => await _service.AddCommentaryToMovieAsync(User.FindFirstValue(ClaimTypes.Email), postInput);

        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult> RemoveCommentaryToMovieAsync([FromBody] int movieId)
            => await _service.RemoveCommentaryToMovieAsync(User.FindFirstValue(ClaimTypes.Email), movieId);

        [HttpGet]
        [Route("{movieId}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<PostDisplay>>> GetMovieComments([FromRoute] int movieId)
            => await _service.GetMovieComments(movieId);
    }
}
