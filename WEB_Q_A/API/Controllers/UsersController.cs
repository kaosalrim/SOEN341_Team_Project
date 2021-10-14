using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using API.Entities;
using API.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<ApplicationUser> SignInManager;

        public UsersController(UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> SignInManager, IConfiguration config)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            this._config = config;
        }

        [Authorize]
        [HttpGet]
        [Route("id")]
        //Get = api/users/id
        //Example = api/users/a0ec2c4f-d9ff-4627-b248-fb2f590148cd
        public async Task<object> GetUsers(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null) return BadRequest();
            
            ClientUser c1 = new ClientUser();
            c1.username = user.UserName;
            return c1;
        }

        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public async void Logout()
        {
            await HttpContext.SignOutAsync();

        }

        [HttpPost]
        [Route("Login")]
        //Post request for login 
        //api/users/Login
        public async Task<Object> LogIn(LoginDataDto loginDto)
        {
            var user = await UserManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return BadRequest();

            var result = await SignInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return BadRequest(result);

            var claims = new List<Claim>();
            claims.Add(new Claim("username", loginDto.UserName));
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("FirstName", user.FirstName));
            claims.Add(new Claim("LastName", user.LastName));
            claims.Add(new Claim("DateJoined", user.DateJoined.ToString("s")));
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(claimPrincipal);
            return Ok(new { result = result });
        }

        [HttpPost]
        [Route("Register")]
        //POST = api/users/Register
        public async Task<object> PostApplicationUser(RegisterDto model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FristName,
                LastName = model.LastName,
                DateJoined = DateTime.Now,
            };

            var result = await UserManager.CreateAsync(applicationUser, model.Password);
            return Ok(result);
        }
    }
}