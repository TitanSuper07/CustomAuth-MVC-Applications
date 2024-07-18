namespace CustomAuth3.Interface
{
    public interface IJwtService
    {
        string GenerateJwtToken(string userId, string username, IEnumerable<string> roles);
    }

}
