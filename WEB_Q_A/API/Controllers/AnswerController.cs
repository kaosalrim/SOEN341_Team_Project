using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AnswersController : BaseApiController
    {
        private readonly DataContext _context;
        public AnswersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetAnswers()
        {
            return "returns all answers";
        }

        // api/Answers/get-user-answers/3
        [Authorize]
        [HttpGet("get-user-answers/{id}")]
        public async Task<ActionResult<string>> GetUsersAnswers(int id)
        {
            return "This returns all answers of particular user";
        }

        // api/Answers/get-q-answers/3
        [HttpGet("get-q-answers/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> GetQuestionAnswers(int id)
        {
            return "This returns all answers to particular question";
        }
    }
}