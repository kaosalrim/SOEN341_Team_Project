using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class QuestionsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions([FromQuery]UserParams userParams)
        {
            var questions = await _unitOfWork.QuestionRepository.GetQuestionsAsync(userParams);
            Response.AddPaginationHeader(questions.CurrentPage, questions.PageSize,
             questions.TotalCount, questions.TotalPages);
            return Ok(questions);
        }

        // api/questions/user/bob
        [Authorize]
        [HttpGet("user/{username}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetUserQuestions(string username,
         [FromQuery]UserParams userParams)
        {
            var questions = await _unitOfWork.QuestionRepository.GetQuestionsByUsernameAsync(username, userParams);
            Response.AddPaginationHeader(questions.CurrentPage, questions.PageSize,
             questions.TotalCount, questions.TotalPages);
            return Ok(questions);
        }

        [Authorize]
        [HttpGet("user-answered/{username}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetUserQuestionsAnswered(string username,
         [FromQuery]UserParams userParams)
        {
            var questions = await _unitOfWork.QuestionRepository.GetUserQuestionsAnsweredAsync(username, userParams);
            Response.AddPaginationHeader(questions.CurrentPage, questions.PageSize,
             questions.TotalCount, questions.TotalPages);
            return Ok(questions);
        }

        // api/questions/3
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            return await _unitOfWork.QuestionRepository.GetQuestionByIdAsync(id);
        }

        [AllowAnonymous]
        [HttpGet("get-user-photo/{username}")]
        public async Task<ActionResult<PhotoDto>> GetUserQuestionPhoto(string username)
        {
            return await _unitOfWork.QuestionRepository.GetUserQuestionPhoto(username);
        }

        [AllowAnonymous]
        [HttpGet("get-user-rep/{username}")]
        public async Task<ActionResult<string>> GetUserQuestionRep(string username)
        {
            return await _unitOfWork.QuestionRepository.GetUserQuestionRep(username);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuestion(int id, QuestionUpdateDto questionUpdateDto){
            var question = await _unitOfWork.QuestionRepository.GetQuestionEntityByIdAsync(id);
            _mapper.Map(questionUpdateDto, question);
            _unitOfWork.QuestionRepository.Update(question);

            if(await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update the question");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(QuestionCreateDto questionCreateDto){
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(questionCreateDto.Username);

            if (user == null) return BadRequest("Failed to create the question");

            var question = new Question();
            questionCreateDto.AppUserId = user.Id;

            question = _mapper.Map(questionCreateDto, question);

            _unitOfWork.QuestionRepository.Create(question);

            if (await _unitOfWork.Complete()) {
                return await _unitOfWork.QuestionRepository.GetQuestionByIdAsync(question.Id);
            }

            return BadRequest("Failed to create the question");
        }
    }
}
