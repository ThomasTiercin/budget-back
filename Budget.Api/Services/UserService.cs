using Budget.Api.Models;
using Budget.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Budget.Api.Services
{
    public class UserService : IUserService
    {
        private readonly BudgetDbContext _dbContext;
        public UserService(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Login(string userName, string password)
        {
            var user = GetUsers().SingleOrDefault(x => x.UserName == userName);
            // check password return null if password is false
            if (CommonMethods.ConvertToDecrypt(user.Password) != password)
            {
                return string.Empty;
            }
            // return null if user not found
            if (user == null)
            {
                return string.Empty;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Startup.SECRET);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddYears(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            User userConnected = user;
            UpdateUser(userConnected);
            return user.Token;
        }

        public void DeleteUser(int Id)
        {
            var user = _dbContext.User.Find(Id);
            _dbContext.User.Remove(user);
            Save();
        }

        public User GetUserByID(int Id)
        {
            return _dbContext.User.Find(Id);
        }

        public User GetUserByUserName(string username)
        {
            return _dbContext.User.FirstOrDefault(b => b.UserName == username); 
        }


        public IEnumerable<User> GetUsers()
        {
            return _dbContext.User.ToList();
        }

        bool DoesExist(string username)
        {
            return GetUsers().Any(x => x.UserName.Equals(username));
        }

        public void AddUser(User user)
        {
            if (!DoesExist(user.UserName))
            {
                user.Password = CommonMethods.ConvertToEncrypt(user.Password);
                _dbContext.Add(user);
                Save();
            }
            
        }

        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (InvalidCastException)
            {
                
                // Perform some action here, and then throw a new exception.
                throw;
            }
            
        }

        public void UpdateUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            Save();
        }
    }
}
