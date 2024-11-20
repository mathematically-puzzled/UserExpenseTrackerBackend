using Domain;

namespace Application.Repository
{
    public interface IUserAuthRepository
    {
        Task UserLoginAsync(string Username, string Password);
        Task RegisterUserAsync(User User);
        Task ForgotPasswordAsync(string Username, long MobileNumber);
        Task<bool> IsUserPresentAsync(User User);
        Task<bool> UpdateUserAsync(User User);
        Task<bool> DeletUserAsync(Guid Id);
        Task<User> GetUserByIdAsync(Guid Id);
    }
}
