using System;
using System.Collections.Generic;
using System.Linq;
using Budget.Api.Models;
using Budget.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // POST api/user/login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            var token = _service.Login(user.UserName, user.Password);

            if (token == null || token == String.Empty)
                return BadRequest(new { message = "User name or password is incorrect" });

            return Ok(token);
        }

        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;

        }
        [Authorize(Roles = "admin")]
        [HttpGet("/api/Users")]
        public IEnumerable<User> GetUsers()
        {
            return _service.GetUsers().ToList();
        }
        [Authorize(Roles = "admin")]
        [HttpGet("/api/Users/{id}")]
        public ActionResult<User> GetUserByID(Guid id)
        {
            var result = _service.GetUserByID(id);
            if (result is null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpGet("/api/User/{userName}")]
        public ActionResult<User> GetUserByUserName(string username)
        {
            return _service.GetUserByUserName(username);
        }

        [HttpPost("/api/Users")]
        public ActionResult<User> AddUser(User user)
        {
            if (user.Id == Guid.Empty)
            {
                user.Id = Guid.NewGuid();
            }
            _service.AddUser(user);
            return user;
        }
        [Authorize(Roles = "admin")]
        [HttpPut("/api/Users/{id}")]
        public ActionResult<User> UpdateUser(User user)
        {
            _service.UpdateUser(user);
            return user;
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("/api/Users/{id}")]
        public ActionResult<Guid> DeleteUser(Guid id)
        {
            _service.DeleteUser(id);
            return id;
        }
    }
}
