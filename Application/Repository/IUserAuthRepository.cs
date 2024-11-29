using Domain;

namespace Application.Repository
{
    public interface IUserAuthRepository
    {
        Task<User> UserLoginAsync(string UserEmail, string Password);
        Task<bool> RegisterUserAsync(User User);
        Task<bool> ForgotPasswordAsync(User User);
        Task<bool> UpdateUserAsync(User User);
        Task DeletUserAsync(Guid Id);
        Task<User> GetUserByIdAsync(Guid Id);
        Task<string> GenerateJWTToken(User User);
    }
}
