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
            try
            {
                User UserInDb = await _context.UserTable.AsQueryable()
                                .Where(u => u.EmailId == UserEmail)
                                .FirstOrDefaultAsync();
                if (UserInDb != null)
                {
                    if (UserInDb.Password == Password) return UserInDb;
                    throw new Exception("UserEmail and Passwords do not match.");
                }
                throw new Exception("User does not exist in the Database");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ForgotPasswordAsync(User User)
        {
            User UserInDb = await _context.UserTable.AsQueryable()
                .Where(u => u.EmailId == User.EmailId)
                .FirstOrDefaultAsync();
            if (UserInDb != null)
            {
                if (UserInDb.MobileNumber == User.MobileNumber)
                {
                    UserInDb.Password = User.Password;
                    _context.UserTable.Update(UserInDb);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new ArgumentException("User fields entered do not match in the backend.");
            }
            return false;
        }

        public async Task DeletUserAsync(Guid Id)
        {
            try
            {
                User UserInDb = await GetUserByIdAsync(Id);
                _context.UserTable.Remove(UserInDb);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            User UserinDb = await _context.UserTable
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();
            if (UserinDb == null) throw new Exception("User not found in the Database");
            return UserinDb;
        }

        public async Task<bool> RegisterUserAsync(User User)
        {
            try
            {
                bool IsUserInDb = await _context.UserTable.AsQueryable()
                .AnyAsync(u => u.EmailId == User.EmailId);
                if (IsUserInDb) throw new Exception("User already registered with this Email Address.");
                User.JoinedDate = DateTime.Now;
                await _context.AddAsync(User);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
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
