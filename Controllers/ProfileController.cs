using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LettercaixaAPI.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;
        public ProfileController(IProfileService service) => _service = service;

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<Profile>> RegisterProfileAsync(ProfileDTO profileInput)
            => await _service.RegisterProfileAsync(profileInput);

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> LoginAsync(string password, string email)
            => await _service.LoginAsync(password, email);

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteProfileAsync(string email, string password)
            => await _service.DeleteProfileAsync(User.FindFirstValue(ClaimTypes.Email), password);

        [HttpPut]
        [Route("update/email")]
        public async Task<ActionResult<Profile>> UpdateProfileEmailAsync(string currentEmail, string newEmail)
            => await _service.UpdateProfileEmailAsync(User.FindFirstValue(ClaimTypes.Email), newEmail);

        [HttpPut]
        [Route("update/password")]
        public async Task<ActionResult> UpdateProfilePasswordAsync(string email, string currentPassword, string newPassword)
            => await _service.UpdateProfilePasswordAsync(User.FindFirstValue(ClaimTypes.Email), currentPassword, newPassword);

        [HttpPut]
        [Route("profile-picture")]
        public async Task<ActionResult<Profile>> AddOrUpdateProfilePictureAsync(string email, string pictureUrl)
            => await _service.AddOrUpdateProfilePictureAsync(User.FindFirstValue(ClaimTypes.Email), pictureUrl);

        [HttpGet]
        [Route("my-account")]
        public async Task<ActionResult<ProfileDisplay>> GetProfileAsync(string email)
            => await _service.GetProfileAsync(User.FindFirstValue(ClaimTypes.Email));

        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<List<ProfileDisplay>>> GetProfilesByNameAsync([FromRoute] string name)
            => await _service.GetProfilesByNameAsync(name);
    }
}
