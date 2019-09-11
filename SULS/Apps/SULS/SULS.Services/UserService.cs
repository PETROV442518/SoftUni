using SULS.Data;
using SULS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.Services
{
    public class UserService : IUserService
    {
        private readonly SULSContext context;

        public UserService(SULSContext context)
        {
            this.context = context;
        }

        public User CreateUser(User user)
        {
            user = this.context.Users.Add(user).Entity;
            this.context.SaveChanges();

            return user;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            User userFromDb = this.context.Users
                .SingleOrDefault(user => (user.Username == username || user.Email == username) && user.Password == password);

            return userFromDb;
        }

        public User GetUserById(string userId)
        {
            return this.context.Users.FirstOrDefault(a => a.Id == userId);
        }
    }
}
