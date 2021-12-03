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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AnswersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // api/answers/user/bob
        [Authorize]
        [HttpGet("user/{username}")]
        public async Task<ActionResult<AnswerDto>> GetUserAnswers(string username)
        {
            var answers = await _unitOfWork.AnswerRepository.GetAnswersByUsernameAsync(username);
            return Ok(answers);
        }

        // api/answers/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswer(int id)
        {
            return await _unitOfWork.AnswerRepository.GetAnswerByIdAsync(id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnswer(int id, AnswerUpdateDto answerUpdateDto)
        {
            var answer = await _unitOfWork.AnswerRepository.GetAnswerEntityByIdAsync(id);
            _mapper.Map(answerUpdateDto, answer);
            _unitOfWork.AnswerRepository.Update(answer);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update the answer");
        }

        [Authorize]
        [HttpPut("updatebestanswer/{id}")]
        public async Task<ActionResult> UpdateBestAnswer(int id, AnswerUpdateDto answerUpdateDto)
        {
            var answer = await _unitOfWork.AnswerRepository.GetAnswerEntityByIdAsync(id);

            _mapper.Map(answerUpdateDto, answer);
            _unitOfWork.AnswerRepository.UpdateBestAnswer(answer);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update the answer");
        }

        [Authorize]
        [HttpPut("updaterank/{id}/{upvote}/{username}")]
        public async Task<ActionResult> UpdateAnswerRank(int id, bool upvote, string username, AnswerUpdateDto answerUpdateDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);

            if (user == null) return BadRequest("Failed to update the answer");

            var votes = user.UserVotes.ToList().Any(v => v.AnswerId == id && v.AppUserId == user.Id);
            if(votes && upvote) return BadRequest("User already voted.");
            if(!votes && !upvote) return BadRequest("User never voted.");

            var answer = await _unitOfWork.AnswerRepository.GetAnswerEntityByIdAsync(id);
            _mapper.Map(answerUpdateDto, answer);
            _unitOfWork.AnswerRepository.UpdateRank(answer, user.Id, upvote);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update the answer");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateAnswer(AnswerCreateDto answerCreateDto)
        {
            var questionExists = await _unitOfWork.AnswerRepository.IsQuestionExists(answerCreateDto.QuestionId);
            if (!questionExists) return BadRequest("Failed to create the answer");

            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(answerCreateDto.Username);

            if (user == null) return BadRequest("Failed to create the answer");

            var answer = new Answer();
            answerCreateDto.AppUserId = user.Id;

            _mapper.Map(answerCreateDto, answer);

            _unitOfWork.AnswerRepository.Create(answer);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update the answer");
        }
    }
}