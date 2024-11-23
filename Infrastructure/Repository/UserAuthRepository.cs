using Application.Repository;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repository
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserAuthRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> UserLoginAsync(string UserEmail, string Password)
        {
            User UserInDb = await _context.UserTable.AsQueryable()
                .Where(u => u.EmailId == UserEmail)
                .FirstOrDefaultAsync();
            if (UserInDb != null)
            {
                if (UserInDb.Password == Password) return UserInDb;
                else return null;
            }
            return null;
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

        public async Task<bool> RegisterUserAsync(User User)
        {
            bool IsUserInDb = await _context.UserTable.AsQueryable()
                .AnyAsync(u => u.EmailId == User.EmailId);
            if (IsUserInDb) return false;
            User.JoinedDate = DateTime.Now;
            await _context.AddAsync(User);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(User User)
        {
            _context.UserTable.Update(User);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<string> GenerateJWTToken(User User)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, User.Name),
                new Claim(JwtRegisteredClaimNames.Email,User.EmailId),
                new Claim("DateOfJoining",User.JoinedDate.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var Token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: Credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(Token);

            return Task.FromResult(jwtToken);
        }
    }
}
