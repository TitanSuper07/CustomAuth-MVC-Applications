using CustomAuth3.Database;
using CustomAuth3.Interface;
using CustomAuth3.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;

    public UserService(ApplicationDbContext context, IConfiguration configuration, IJwtService jwtService)
    {
        _context = context;
        _configuration = configuration;
        _jwtService = jwtService;
    }

    public async Task<string> RegisterAsync(RegistartionViewModel model)
    {
        string Case = null;
        // Hash the password securely
        string hashedPassword = HashPassword(model.Password);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
        var userByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null && userByEmail==null) { 

        var newUser = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            Password = hashedPassword,
            Department = model.Department,
            RoleId = 2
        };

        // Add user to DbContext and save changes
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        Case = "true";
        return Case ;
        }
        else
        {
            Case = "AlreadyExists";
            return Case;
        }
    }

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        var userByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);

        if (user == null && userByEmail == null)
        {
            return null;
        }

        if (user != null)
        {
            if (!VerifyPassword(password, user.Password))
            {
                return null;
            }

            return _jwtService.GenerateJwtToken(user.UserId.ToString(), user.UserName, GetRoles(user));
        }
        else if (userByEmail != null)
        {
            if (!VerifyPassword(password, userByEmail.Password))
            {
                return null;
            }
            return _jwtService.GenerateJwtToken(userByEmail.UserId.ToString(), userByEmail.UserName, GetRoles(userByEmail));

        }

        string Case = "notIncludedCase";
        return Case;
    }


    // Other methods for updating user information, managing roles, etc.

    public string HashPassword(string password)
    {
        // Use a secure hashing algorithm like BCrypt or PBKDF2
        // Example using BCrypt:
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        // Verify password against hashed password
        // Example using BCrypt:
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public IEnumerable<string> GetRoles(User user)
    {
        // Retrieve user roles based on user data
        // Example: query roles from database or use predefined roles
        List<string> roles = new List<string>();

        // Example logic to get roles based on user's RoleId
        if (user.RoleId == 1)
        {
            roles.Add("Admin");
        }
        else if (user.RoleId == 2)
        {
            roles.Add("NormalUser");
        }

        return roles;
    }

    //string IUserService.HashPassword(string password)
    //{
    //    throw new NotImplementedException();
    //}

    //bool IUserService.VerifyPassword(string password, string hashedPassword)
    //{
    //    throw new NotImplementedException();
    //}

    //IEnumerable<string> IUserService.GetRoles(User user)
    //{
    //    throw new NotImplementedException();
    //}
}
