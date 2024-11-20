using Application.Repository;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task UserLoginAsync(string Username, string Password)
        {
            throw new NotImplementedException();
        }
        public Task ForgotPasswordAsync(string Username, long MobileNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletUserAsync(Guid Id)
        {
            User UserInDb = await GetUserByIdAsync(Id);
            if (UserInDb != null)
            {
                _context.UserTable.Remove(UserInDb);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            return await _context.UserTable
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserPresentAsync(User User)
        {
            return await _context.UserTable.AnyAsync(u => u.EmailId == User.EmailId || u.Id == User.Id);
        }

        public async Task RegisterUserAsync(User User)
        {
            await _context.AddAsync(User);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsync(User User)
        {
            _context.UserTable.Update(User);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
