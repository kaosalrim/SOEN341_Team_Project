using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AnswerRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Answer> GetAnswerEntityByIdAsync(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<IEnumerable<Answer>> GetAnswerEntitiesByUsernameAsync(string username)
        {
            return await _context.Answers
            .Include(p => p.Question)
            .Where(x => x.AppUser.UserName == username)
            .ToListAsync();
        }

        public async Task<IEnumerable<Answer>> GetAnswerEntitiesAsync()
        {
            return await _context.Answers
            .Include(p => p.Question)
            .ToListAsync();
        }

        public async Task<AnswerDto> GetAnswerByIdAsync(int id)
        {
            return await _context.Answers.Where(u => u.Id == id)
            .ProjectTo<AnswerDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AnswerDto>> GetAnswersByUsernameAsync(string username)
        {
            return await _context.Answers.Where(u => u.AppUser.UserName == username)
            .ProjectTo<AnswerDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<IEnumerable<AnswerDto>> GetAnswersAsync()
        {
            return await _context.Answers.ProjectTo<AnswerDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void Update(Answer answer)
        {
            _context.Entry(answer).State = EntityState.Modified;
        }

        public void UpdateBestAnswer(Answer answer)
        {
            _context.Entry(answer).State = EntityState.Modified;
            if (answer.IsBestAnswer)
            {
                var answers = _context.Answers.Where(x => x.QuestionId == answer.QuestionId && x.Id != answer.Id);
                foreach (var ans in answers)
                {
                    ans.IsBestAnswer = false;
                    _context.Entry(ans).State = EntityState.Modified;
                }
            }
        }

        public void UpdateRank(Answer answer, int userId, bool upvote)
        {
            _context.Entry(answer).State = EntityState.Modified;
            if (upvote)
            {
                _context.Entry(new UserVotes()
                {
                    AnswerId = answer.Id,
                    AppUserId = userId
                }).State = EntityState.Added;
            }
            else
            {
                var vote = _context.UserVotes.SingleOrDefault(x => x.AnswerId == answer.Id && x.AppUserId == userId);
                if (vote != null)
                {
                    _context.Entry(vote).State = EntityState.Deleted;
                }
            }
        }

        public void Create(Answer answer)
        {
            _context.Entry(answer).State = EntityState.Added;
        }
        public async Task<bool> IsQuestionExists(int questionId)
        {
            return await _context.Questions.AnyAsync(x => x.Id == questionId);
        }
    }
}