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

        /// <summary>
        /// Method to login user.
        /// </summary>
        /// <param name="UserEmail"></param>
        /// <param name="Password"></param>
        /// <returns>
        /// Returns User when found, returns null when not found.
        /// </returns>
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

        /// <summary>
        /// Method to reset Password for User.
        /// </summary>
        /// <param name="User"></param>
        /// <returns>
        /// Returns True when record Updated, false if not.
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
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
                throw new ArgumentException("User fields entered do not match in the backend");
            }
            return false;
        }

        /// <summary>
        /// Method to delete User Async.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// If UserInDb return True then deleted; false if not present in Db
        /// </returns>
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

        /// <summary>
        /// Method to Fetch User by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>User from Table</returns>
        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            return await _context.UserTable
                .Where(p => p.Id == Id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Method to Register User in Database. Puts DateTime.Now so that User has no control
        /// over it.
        /// </summary>
        /// <param name="User"></param>
        /// <returns>
        /// Returns True when record added; False when User is already in Db.
        /// </returns>
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

        /// <summary>
        /// Method to Update User Async.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAsync(User User)
        {
            _context.UserTable.Update(User);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Generate JWT Token for the said User. Add necessary claims for the User.
        /// </summary>
        /// <param name="User"></param>
        /// <returns>
        /// Bearer Token
        /// </returns>
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
