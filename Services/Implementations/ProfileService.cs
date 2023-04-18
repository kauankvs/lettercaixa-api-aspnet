using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BC = BCrypt.Net.BCrypt;

namespace LettercaixaAPI.Services.Implementations
{
    public class ProfileService: IProfileService
    {
        private readonly LettercaixaContext _context;
        private readonly IAuthService _auth;

        public ProfileService(LettercaixaContext context, IAuthService auth)
        {
            _context = context;
            _auth = auth;
        }

        public async Task<ActionResult<Profile>> RegisterProfileAsync(ProfileDTO profileInput)  
        { 
            bool userExists = await _auth.VerifyIfProfileExistsAsync(profileInput.Email);
            if (userExists.Equals(true))
                return new ConflictObjectResult(profileInput.Email);

            string passwordHash = BC.HashPassword(profileInput.Password);
            Profile profile = new Profile()
            {
                FirstName = profileInput.FirstName,
                LastName = profileInput.LastName,
                Email = profileInput.Email,
                PasswordHash = passwordHash,
                ProfilePicture = profileInput.ProfilePicture,
                Birth = profileInput.Birth,
                Username = profileInput.Username,
            };
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            return new OkObjectResult(profile);
        }

        public async Task<ActionResult<string>> LoginAsync(string password, string email)
        {
            bool userExists = await _auth.VerifyIfProfileExistsAsync(email);
            if (userExists.Equals(false))
                return new BadRequestObjectResult(email);

            bool passwordIsCorrect = await _auth.VerifyIfPasswordIsEqualAsync(email, password);
            if (passwordIsCorrect.Equals(false))
                return new BadRequestObjectResult(password);
           
            string token = await _auth.CreateTokenAsync(email);
            return new  OkObjectResult(token);
        }

        public async Task<ActionResult> DeleteProfileAsync(string email, string password)
        {
            Profile? profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Email.Equals(email));
            bool passwordIsCorrect = BC.Verify(password, profile.PasswordHash);
            if(passwordIsCorrect.Equals(false))
                return new BadRequestObjectResult(password);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return new AcceptedResult();
        }

        public async Task<ActionResult<Profile>> UpdateProfileEmailAsync(string currentEmail, string newEmail) 
        {
            bool newEmailAlreadyExists = await _auth.VerifyIfProfileExistsAsync(newEmail);
            if(newEmailAlreadyExists.Equals(true))
                return new BadRequestObjectResult(newEmail);

            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(currentEmail));
            profile.Email = newEmail;
            await _context.SaveChangesAsync();
            return new OkObjectResult(profile);
        }

        public async Task<ActionResult> UpdateProfilePasswordAsync(string email, string currentPassword, string newPassword)
        {
            bool currentPasswordIsCorrect = await _auth.VerifyIfPasswordIsEqualAsync(email, currentPassword);
            if (currentPasswordIsCorrect.Equals(false))
                return new BadRequestObjectResult(currentPassword);

            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            string newPasswordHash = BC.HashPassword(newPassword);
            profile.PasswordHash = newPasswordHash;

            await _context.SaveChangesAsync();
            return new OkObjectResult(newPassword);
        }

        public async Task<ActionResult<Profile>> AddOrUpdateProfilePictureAsync(string email, string pictureUrl) 
        {
            Profile profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email.Equals(email));
            profile.ProfilePicture = pictureUrl;
            await _context.SaveChangesAsync();
            return new OkObjectResult(profile);
        }

        public async Task<ActionResult<Profile>> GetProfileAsync(string email)
        {
            Profile profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Email.Equals(email));
            profile.PasswordHash = "********";
            return new OkObjectResult(profile);
        }

        public async Task<ActionResult<List<ProfileDisplay>>> GetProfilesByNameAsync(string name) 
        {
            List<Profile> profiles = await _context.Profiles.AsNoTracking()
                .Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name) || p.Username.Contains(name))
                .ToListAsync();

            if (profiles.Equals(null))
                return new NoContentResult();

            List<ProfileDisplay> usersProfile = new List<ProfileDisplay>();
            foreach(Profile profile in profiles)
            {
                usersProfile.Add(new ProfileDisplay()
                {
                    ProfileId = profile.ProfileId,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Username = profile.Username,
                    ProfilePicture = profile.ProfilePicture,
                });
            }
            return new OkObjectResult(usersProfile);
        }

        public async Task<ActionResult<ProfileDisplay>> GetProfileById(int profileId)
        {
            Profile? profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.ProfileId == profileId);
            if(profile == null)
                return new NoContentResult();

            return new OkObjectResult(profile);
        }
    } 
}
