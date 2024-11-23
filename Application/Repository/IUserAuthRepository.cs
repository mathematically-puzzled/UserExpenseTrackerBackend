using Domain;

namespace Application.Repository
{
    public interface IUserAuthRepository
    {
        Task<string> UserLoginAsync(User User);
        Task RegisterUserAsync(User User);
        Task ForgotPasswordAsync(string Username, long MobileNumber);
        Task<bool> IsUserPresentAsync(string EmailId);
        Task<bool> UpdateUserAsync(User User);
        Task<bool> DeletUserAsync(Guid Id);
        Task<User> GetUserByIdAsync(Guid Id);
        Task<User> GetUserByEmailId(string EmailId);
        Task<string> GenerateJWTToken(User User);
    }
}
