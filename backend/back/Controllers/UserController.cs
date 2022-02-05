using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;

        public UserController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            using (var service = new CryptoContext())
            {
                var userList = service.Users.ToList();
                return userList;
            }
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel user)
        {
            bool result = false;

            using (var service = new CryptoContext())
            {
                service.Users.Add(mapper.Map<User>(user));
                service.SaveChanges();
                result = true;
            }
            return Ok(result);
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser([FromBody] LoginUserModel user)
        {
            using (var service = new CryptoContext())
            {
                var login = service.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                if (login is null)
                {
                    return Ok(null);
                }
                return Ok(login);
            }
        }
    }
}
