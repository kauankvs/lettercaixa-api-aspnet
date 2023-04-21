using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<ActionResult<Profile>> RegisterProfileAsync([FromForm] ProfileDTO profileInput)
            => await _service.RegisterProfileAsync(profileInput);

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginAsync([FromForm] ProfileLogin profile)
            => await _service.LoginAsync(profile.Password, profile.Email);

        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult> DeleteProfileAsync([FromForm] string password)
            => await _service.DeleteProfileAsync(User.FindFirstValue(ClaimTypes.Email), password);

        [HttpPut]
        [Route("update/email")]
        [Authorize]
        public async Task<ActionResult<Profile>> UpdateProfileEmailAsync([FromForm] string newEmail)
            => await _service.UpdateProfileEmailAsync(User.FindFirstValue(ClaimTypes.Email), newEmail);

        [HttpPut]
        [Route("update/password")]
        [Authorize]
        public async Task<ActionResult> UpdateProfilePasswordAsync(string currentPassword, string newPassword)
            => await _service.UpdateProfilePasswordAsync(User.FindFirstValue(ClaimTypes.Email), currentPassword, newPassword);

        [HttpPut]
        [Route("/profile-picture")]
        [Authorize]
        public async Task<ActionResult<Profile>> AddOrUpdateProfilePictureAsync(string pictureUrl)
            => await _service.AddOrUpdateProfilePictureAsync(User.FindFirstValue(ClaimTypes.Email), pictureUrl);

        [HttpGet]
        [Route("my-account")]
        [Authorize]
        public async Task<ActionResult<Profile>> GetProfileAsync()
            => await _service.GetProfileAsync(User.FindFirstValue(ClaimTypes.Email));

        [HttpGet]
        [Route("{name}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Profile>>> GetProfilesByNameAsync([FromRoute] string name)
            => await _service.GetProfilesByNameAsync(name);

        [HttpGet]
        [Route("{profileId}")]
        [AllowAnonymous]
        public async Task<ActionResult<Profile>> GetProfileByIdAsync([FromRoute] int profileId)
            => await _service.GetProfileByIdAsync(profileId);

        [HttpGet]
        [Route("/all")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Profile>>> GetAllProfilesAsync()
            => await _service.GetAllProfilesAsync();
    }
}
