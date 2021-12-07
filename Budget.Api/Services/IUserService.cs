using Budget.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Api.Services
{
    public interface IUserService
    {
        string Login(string userName, string password);

        IEnumerable<User> GetUsers();

        void AddUser(User userItem);
        User GetUserByID(int id);
        User GetUserByUserName(string username);
        
        void UpdateUser(User userItem);

        void DeleteUser(int id);
    }
}
