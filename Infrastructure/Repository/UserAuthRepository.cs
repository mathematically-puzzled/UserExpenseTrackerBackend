using Application.Models;
using Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserAuthRepository : IUserAuthRepository
    {
        public Task ForgotPassword(string Username, long MobileNumber)
        {
            throw new NotImplementedException();
        }

        public Task IsUserPresent(string Username)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(NewUser User)
        {
            throw new NotImplementedException();
        }

        public Task UserLogin(string Username, string Password)
        {
            throw new NotImplementedException();
        }
    }
}
