using CustomAuth3.Models;

namespace CustomAuth3.Interface
{
    public interface IUserService
    {
        Task<String> RegisterAsync(RegistartionViewModel model);
        Task<string> AuthenticateAsync(string username, string password);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        IEnumerable<string> GetRoles(User user);

    }
}
