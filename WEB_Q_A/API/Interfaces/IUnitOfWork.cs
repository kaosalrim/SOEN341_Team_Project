using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get; }
        IQuestionRepository QuestionRepository{get;}
        IAnswerRepository AnswerRepository{get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}