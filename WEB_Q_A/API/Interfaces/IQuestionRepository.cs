using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IQuestionRepository
    {
        void Update(Question question);
        void Create(Question question);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Question>> GetQuestionEntitiesAsync();
        Task<Question> GetQuestionEntityByIdAsync(int id);
        Task<IEnumerable<Question>> GetQuestionEntitiesByUsernameAsync(string username);
        Task<PagedList<QuestionDto>> GetQuestionsAsync(UserParams userParams);
        Task<QuestionDto> GetQuestionByIdAsync(int id);
        Task<PagedList<QuestionDto>> GetQuestionsByUsernameAsync(string username, UserParams userParams);
        Task<PagedList<QuestionDto>> GetUserQuestionsAnsweredAsync(string username, UserParams userParams);
        Task<PhotoDto> GetUserQuestionPhoto(string username);
        Task<string> GetUserQuestionRep(string username);
    }
}