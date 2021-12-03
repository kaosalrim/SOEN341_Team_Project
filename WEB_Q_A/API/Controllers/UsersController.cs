using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetMembersAsync();
            return Ok(users);
        }

        // api/users/3
        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetMemberAsync(username);
        }
    }
}