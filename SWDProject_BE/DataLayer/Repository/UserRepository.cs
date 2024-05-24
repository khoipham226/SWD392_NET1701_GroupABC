using DataLayer.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class UserRepository
    {
        private static ConcurrentBag<User> Users = new ConcurrentBag<User>();

        public User AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            Users.Add(user);
            return user;
        }

        public User GetUser(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public bool UsernameExists(string username)
        {
            return Users.Any(u => u.Username == username);
        }
    }
}
