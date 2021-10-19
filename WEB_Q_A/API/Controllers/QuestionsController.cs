using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using API.DTOs;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]

	public class QuestionsController : ControllerBase
	{
		private readonly DataContext _context;
		private int _currentUserId;

		//constructor
		public QuestionsController(DataContext context, int currentUserId)
		{
			_context = context;
			_currentUserId = currentUserId;
		}

		
		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<QuestionModel>>> GetQuestions()
		{
			return await _context.Question.ToListAsync(); //get all questions in database
		}

		
		[HttpGet("get-user-questions/{userId}")]
		[Authorize]
		public async Task<ActionResult<IEnumerable<QuestionModel>>> GetUsersQuestions(int userId)
		{
            if (_currentUserId == userId)
            {
				return await _context.Question.AsQueryable().Where(q => q.UserId == userId).ToListAsync();
			}
            else
            {
				return Unauthorized(); //user Id doesn't match current user Id
            }
		}

		
		[HttpGet("get-user-question/{QuestionDto}")]
		[Authorize]
		public async Task<ActionResult<QuestionModel>> GetSingleUserQuestion(QuestionDto questionDto)
		{


			if (_currentUserId == questionDto.UserId)
			{
				return await _context.Question.FindAsync(questionDto.Id);
			}
			else
			{
				return Unauthorized(); //user Id doesn't match current user Id
			}
		}

		
		
		[HttpPost("post-user-question/{QuestionModel}")]
		[Authorize]
		public async Task<ActionResult<QuestionModel>> PostQuestion(QuestionModel question) //could use DTOs
		{

			if (_currentUserId == question.UserId)
			{
				if (question == null)
				{
					return BadRequest(); //the QuestionModel object should not be null
				}
				else
				{
					_context.Question.Add(question);
					await _context.SaveChangesAsync();

					return CreatedAtAction("Get Question", new { id = question.Id }, question);
				}
			}
			else
			{
				return Unauthorized(); //user Id doesn't match current user Id
			}


		}


		[HttpPut("edit-user-question/{QuestionDetailsDto}")]
		[Authorize]
		public async Task<IActionResult> EditUserQuestion(QuestionDetailsDto questionDto)
		{

			if (_currentUserId == questionDto.UserId)
			{

				var Question = await _context.Question.FindAsync(questionDto.Id);

				//if the question doesn't exist
				if (Question == null)
				{
					return NotFound(); //can't find the question
				}
				else
				{
					Question.QuestionDetails = questionDto.QuestionDetails; 
					_context.Entry(Question).State = EntityState.Modified; //notify context that data has been modified
					await _context.SaveChangesAsync();

					return NoContent(); //no content expected
				}
			}
			else
			{
				return Unauthorized(); //user Id doesn't match current user Id
			}
		}

		
		[HttpDelete("delete-user-question/{QuestionDto}")]
		[Authorize]
		public async Task<IActionResult> DeleteUserQuestion(QuestionDto questionDto)
		{

			if (_currentUserId == questionDto.UserId)
			{
				var Question = await _context.Question.FindAsync(questionDto.Id);
				var Answers = _context.Answer.AsQueryable().Where(a => a.Id == questionDto.Id); //ansers with matching question Id

				//if the question doesn't exist
				if (Question == null)
				{
					return NotFound(); //can't find the question
				}
				else
				{
					_context.Question.Remove(Question);
					_context.Answer.RemoveRange(Answers);
					await _context.SaveChangesAsync();

					return NoContent(); //no content expected
				}
			}
			else
			{
				return Unauthorized(); //user Id doesn't match current user Id
			}


		}

	}
}
