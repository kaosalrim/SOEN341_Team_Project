using API.Entities;

namespace API.Interfaces
{
    //Generate JSW token interface
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}