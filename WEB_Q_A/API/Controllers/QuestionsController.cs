using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class QuestionsController : BaseApiController
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public QuestionsController(IQuestionRepository questionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questions = await _questionRepository.GetQuestionsAsync();
            return Ok(questions);
        }

        // api/questions/user/bob
        [Authorize]
        [HttpGet("user/{username}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetUserQuestions(string username)
        {
            var questions = await _questionRepository.GetQuestionsByUsernameAsync(username);
            return Ok(questions);
        }

        [Authorize]
        [HttpGet("user-answered/{username}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetUserQuestionsAnswered(string username)
        {
            var questions = await _questionRepository.GetUserQuestionsAnsweredAsync(username);
            return Ok(questions);
        }

        // api/questions/3
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            return await _questionRepository.GetQuestionByIdAsync(id);
        }

        [AllowAnonymous]
        [HttpGet("get-user-photo/{username}")]
        public async Task<ActionResult<PhotoDto>> GetUserQuestionPhoto(string username)
        {
            return await _questionRepository.GetUserQuestionPhoto(username);
        }

        [AllowAnonymous]
        [HttpGet("get-user-rep/{username}")]
        public async Task<ActionResult<string>> GetUserQuestionRep(string username)
        {
            return await _questionRepository.GetUserQuestionRep(username);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuestion(int id, QuestionUpdateDto questionUpdateDto){
            var question = await _questionRepository.GetEQuestionByIdAsync(id);
            _mapper.Map(questionUpdateDto, question);
            _questionRepository.Update(question);

            if(await _questionRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the question");
        }

        public ActionResult PostQuestions(QuestionDto questionDto)
        {
            for (QuestionDto questionDto: QuestionDto )
            {

            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(QuestionCreateDto questionCreateDto){

            var user = await _userRepository.GetUserByUsernameAsync(questionCreateDto.Username);

            if (user == null) return BadRequest("Failed to create the question");

            var question = new Question();
            questionCreateDto.AppUserId = user.Id;

            question = _mapper.Map(questionCreateDto, question);

            _questionRepository.Create(question);

            if (await _questionRepository.SaveAllAsync()) {
                
                return await _questionRepository.GetQuestionByIdAsync(question.Id);
            }

            return BadRequest("Failed to create the question");
        }
    }
}
