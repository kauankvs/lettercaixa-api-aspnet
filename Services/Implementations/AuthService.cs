using LettercaixaAPI.Models;
using LettercaixaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;


namespace LettercaixaAPI.Services.Implementations
{
    public class AuthService: IAuthService
    {
        private readonly LettercaixaContext _context;
        public AuthService(LettercaixaContext context)
            => _context = context;
        
        public async Task<bool> VerifyIfPasswordIsEqualAsync(string loginEmail, string loginPassword) 
        {
            Profile? profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Email.Equals(loginEmail));
            if(profile.Equals(null))
                return false;

            bool passwordIsCorrect = BC.Verify(loginPassword, profile.PasswordHash);
            return passwordIsCorrect;
        }

        public async Task<bool> VerifyIfProfileExistsAsync(string email) 
        {
            bool profileExists = await _context.Profiles.AsNoTracking().AnyAsync(p => p.Email.Equals(email));
            return profileExists;
        }

        public async Task<string> CreateTokenAsync(string email)
        {
            Profile profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
            var chave = Encoding.ASCII.GetBytes(Settings.Key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                }),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
