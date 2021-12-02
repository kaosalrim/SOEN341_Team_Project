using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public AccountController(ITokenService tokenService, UserManager<AppUser> UserManager,
         SignInManager<AppUser> SignInManager, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = UserManager;
            _signInManager = SignInManager;
        }

        [HttpGet("logout")]
        //api/account/logout
        public async void Logout()
        {
            await HttpContext.SignOutAsync();
        }

        [HttpPost("login")]
        //Post request for login 
        //api/account/login
        public async Task<ActionResult<UserDto>> LogIn(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(u => u.Photo)
                .Include(u => u.Questions)
                .Include(u => u.Answers)
                .SingleOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();

            return await MapUserToDto(user);
        }

        [HttpPost("register")]
        //POST = api/account/register
        public async Task<ActionResult<UserDto>> PostApplicationUser(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Username.ToLower();
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return await MapUserToDto(user);
        }

        private async Task<UserDto> MapUserToDto(AppUser user){
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await _tokenService.CreateToken(user);

            return userDto;
        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}