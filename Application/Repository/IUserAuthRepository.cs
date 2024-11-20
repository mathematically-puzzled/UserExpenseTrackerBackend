using Application.Models;

namespace Application.Repository
{
    public interface IUserAuthRepository
    {
        Task UserLogin(string Username, string Password);
        Task RegisterUser(NewUser User);
        Task ForgotPassword(string Username, long MobileNumber);
        Task IsUserPresent(string Username);
    }
}
