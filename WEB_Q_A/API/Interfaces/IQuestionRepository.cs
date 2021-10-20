using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IQuestionRepository
    {
        void Update(Question question);
        void Create(Question question);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Question>> GetEQuestionsAsync();
        Task<Question> GetEQuestionByIdAsync(int id);
        Task<IEnumerable<Question>> GetEQuestionsByUsernameAsync(string username);
        Task<IEnumerable<QuestionDto>> GetQuestionsAsync();
        Task<QuestionDto> GetQuestionByIdAsync(int id);
        Task<IEnumerable<QuestionDto>> GetQuestionsByUsernameAsync(string username);
        Task<IEnumerable<QuestionDto>> GetUserQuestionsAnsweredAsync(string username);
        Task<PhotoDto> GetUserQuestionPhoto(string username);
        Task<string> GetUserQuestionRep(string username);
    }
}