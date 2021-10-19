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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Question> GetEQuestionByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<IEnumerable<Question>> GetEQuestionsByUsernameAsync(string username)
        {
            return await _context.Questions
            .Include(p => p.Answers)
            .Where(x => x.AppUser.UserName == username).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetEQuestionsAsync()
        {
            return await _context.Questions
            .Include(p => p.Answers)
            .ToListAsync();
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions.Where(u => u.Id == id)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsByUsernameAsync(string username)
        {
            return await _context.Questions.Where(u => u.AppUser.UserName == username)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }
        public async Task<IEnumerable<QuestionDto>> GetUserQuestionsAnsweredAsync(string username)
        {
            return await _context.Questions
            .Include(q => q.Answers)
            .Where(q => q.Answers.Any(a => a.AppUser.UserName == username))
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync()
        {
            return await _context.Questions.ProjectTo<QuestionDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Create(Question question)
        {
            _context.Entry(question).State = EntityState.Added;
        }

        public void Update(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
        }

        public async Task<PhotoDto> GetUserQuestionPhoto(string username)
        {
            var user = await _context.Users
            .Include(p => p.Photo)
            .SingleOrDefaultAsync(u => u.UserName == username);
            return new PhotoDto(){
                Id = user.Photo?.Id,
                Url = user.Photo?.Url
            };
        }

        public async Task<string> GetUserQuestionRep(string username)
        {
            var user = await _context.Users
            .Include(q => q.Questions)
            .Include(a => a.Answers)
            .SingleOrDefaultAsync(u => u.UserName == username);

            return (user.Questions.Count + user.Answers.Count).ToString();
        }
    }
}