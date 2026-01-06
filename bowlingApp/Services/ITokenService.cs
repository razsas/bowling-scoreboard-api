using bowlingApp.Models;

namespace bowlingApp.Services
{
    public interface ITokenService
    {
        string? CreateToken(User user);
    }
}
