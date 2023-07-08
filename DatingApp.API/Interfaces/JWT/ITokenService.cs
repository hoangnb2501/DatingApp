using DatingApp.API.Entities;

namespace DatingApp.API.Interfaces.JWT
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}