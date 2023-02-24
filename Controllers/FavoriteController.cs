using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Implementations;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LettercaixaAPI.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _service;
        public FavoriteController(IFavoriteService service) => _service = service;

        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<ActionResult<Favorite>> AddMovieToFavoritesAsync([FromBody] Movie movie)
            => await _service.AddMovieToFavoritesAsync(User.FindFirstValue(ClaimTypes.Email), movie);

        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult> RemoveMovieFromFavoritesAsync([FromBody] Movie movie)
            => await _service.RemoveMovieFromFavoritesAsync(User.FindFirstValue(ClaimTypes.Email), movie);

        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<ActionResult<Favorite>> GetFavoriteMoviesFromProfileAsync()
            => await _service.GetFavoriteMoviesFromProfileAsync(User.FindFirstValue(ClaimTypes.Email));
    }
}
