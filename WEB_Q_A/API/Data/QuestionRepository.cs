using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
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

        public async Task<Question> GetQuestionEntityByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<IEnumerable<Question>> GetQuestionEntitiesByUsernameAsync(string username)
        {
            return await _context.Questions
            .Include(p => p.Answers)
            .Where(x => x.AppUser.UserName == username).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionEntitiesAsync()
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

        public async Task<PagedList<QuestionDto>> GetQuestionsByUsernameAsync(string username, UserParams userParams)
        {
            var query = _context.Questions.Where(u => u.AppUser.UserName == username)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.DatePosted).AsNoTracking();
            return await PagedList<QuestionDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }
        public async Task<PagedList<QuestionDto>> GetUserQuestionsAnsweredAsync(string username, UserParams userParams)
        {
            var query = _context.Questions
            .Include(q => q.Answers)
            .Where(q => q.Answers.Any(a => a.AppUser.UserName == username))
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.DatePosted).AsNoTracking();
            return await PagedList<QuestionDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<QuestionDto>> GetQuestionsAsync(UserParams userParams)
        {
            var query = _context.Questions.ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.DatePosted).AsNoTracking();
            return await PagedList<QuestionDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
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
            return new PhotoDto()
            {
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

            return (user.Questions.Count + user.Answers.Select(a => a.QuestionId).Distinct().Count()).ToString();
        }
    }
}