using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public Task<ActionResult<Profile>> RegisterProfileAsync(ProfileDTO profileInput)
            => _service.RegisterProfileAsync(profileInput);

        [HttpPost]
        [Route("login")]
        public Task<ActionResult<string>> LoginAsync(string password, string email)
            => _service.LoginAsync(password, email);

        [HttpDelete]
        [Route("delete")]
        public Task<ActionResult> DeleteProfileAsync(string email, string password)
            => _service.DeleteProfileAsync(User.FindFirstValue(ClaimTypes.Email), password);

        [HttpPut]
        [Route("update/email")]
        public Task<ActionResult<Profile>> UpdateProfileEmailAsync(string currentEmail, string newEmail)
            => _service.UpdateProfileEmailAsync(User.FindFirstValue(ClaimTypes.Email), newEmail);

        [HttpPut]
        [Route("update/password")]
        public Task<ActionResult> UpdateProfilePasswordAsync(string email, string currentPassword, string newPassword)
            => _service.UpdateProfilePasswordAsync(User.FindFirstValue(ClaimTypes.Email), currentPassword, newPassword);

        [HttpPut]
        [Route("profile-picture")]
        public Task<ActionResult<Profile>> AddOrUpdateProfilePictureAsync(string email, string pictureUrl)
            => _service.AddOrUpdateProfilePictureAsync(User.FindFirstValue(ClaimTypes.Email), pictureUrl);
    }
}
