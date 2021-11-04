using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AnswersController : BaseApiController
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public AnswersController(IAnswerRepository answerRepository, IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _answerRepository = answerRepository;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAnswers()
        // {
        //     var answers = await _answerRepository.GetAnswersAsync();
        //     return Ok(answers);
        // }

        // api/answers/user/bob
        [Authorize]
        [HttpGet("user/{username}")]
        public async Task<ActionResult<AnswerDto>> GetUserAnswers(string username)
        {
            var answers = await _answerRepository.GetAnswersByUsernameAsync(username);
            return Ok(answers);
        }

        // api/answers/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswer(int id)
        {
            return await _answerRepository.GetAnswerByIdAsync(id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnswer(int id, AnswerUpdateDto answerUpdateDto)
        {
            var answer = await _answerRepository.GetEAnswerByIdAsync(id);
            _mapper.Map(answerUpdateDto, answer);
            _answerRepository.Update(answer);

            if (await _answerRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the answer");
        }

        [Authorize]
        [HttpPut("updaterank/{id}/{upvote}/{username}")]
        public async Task<ActionResult> UpdateAnswerRank(int id, bool upvote, string username, AnswerUpdateDto answerUpdateDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            
            if (user == null) return BadRequest("Failed to update the answer");

            var votes = user.UserVotes.ToList().Any(v => v.AnswerId == id && v.AppUserId == user.Id);
            if(votes && upvote) return BadRequest("User already voted.");
            if(!votes && !upvote) return BadRequest("User never voted.");
            

            var answer = await _answerRepository.GetEAnswerByIdAsync(id);
            _mapper.Map(answerUpdateDto, answer);
            _answerRepository.UpdateRank(answer, user.Id, upvote);

            if (await _answerRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the answer");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateAnswer(AnswerCreateDto answerCreateDto)
        {
            var questionExists = await _answerRepository.IsQuestionExists(answerCreateDto.QuestionId);
            if (!questionExists) return BadRequest("Failed to create the answer");

            var user = await _userRepository.GetUserByUsernameAsync(answerCreateDto.Username);

            if (user == null) return BadRequest("Failed to create the answer");

            var answer = new Answer();
            answerCreateDto.AppUserId = user.Id;

            _mapper.Map(answerCreateDto, answer);

            _answerRepository.Create(answer);

            if (await _answerRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the answer");
        }
    }
}