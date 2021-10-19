using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    //Generate JSW token interface
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}