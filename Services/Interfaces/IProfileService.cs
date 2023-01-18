using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LettercaixaAPI.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<ActionResult<Profile>> RegisterProfileAsync(ProfileDTO profileInput);
        public Task<ActionResult<string>> LoginAsync(string password, string email);
        public Task<ActionResult> DeleteProfileAsync(string email, string password);
        public Task<ActionResult<Profile>> UpdateProfileEmailAsync(string currentEmail, string newEmail);
        public Task<ActionResult> UpdateProfilePasswordAsync(string email, string currentPassword, string newPassword);
        public Task<ActionResult<Profile>> AddOrUpdateProfilePictureAsync(string email, string pictureUrl);
        public Task<ActionResult<ProfileDisplay>> GetProfileAsync(string email);
    }
}
