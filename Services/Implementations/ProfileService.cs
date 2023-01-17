using LettercaixaAPI.DTOs;
using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            return new OkObjectResult(token);
        }
    }
}
