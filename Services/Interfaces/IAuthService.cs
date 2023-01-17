namespace LettercaixaAPI.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> VerifyIfPasswordIsEqualAsync(string loginEmail, string loginPassword);
        public Task<bool> VerifyIfProfileExistsAsync(string email);
        public Task<string> CreateTokenAsync(string email);
    }
}
