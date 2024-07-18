using CustomAuth3.Database;
using CustomAuth3.Interface;
using CustomAuth3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly ApplicationDbContext _context;
    public AccountController(IUserService userService, IJwtService jwtService, ApplicationDbContext context)
    {
        _userService = userService;
        _jwtService = jwtService;
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.AuthenticateAsync(model.UserName, model.Password);
            if (user != null)
            {
                User UserFromDb = _context.Users.FirstOrDefault(u => u.UserName == model.UserName);
                User UserFromDbByEmail = _context.Users.FirstOrDefault(u => u.Email == model.UserName);
                IEnumerable<String> roles = null;
                if (UserFromDbByEmail != null)
                {
                    roles = _userService.GetRoles(UserFromDbByEmail);
                }
                else
                {
                    roles = _userService.GetRoles(UserFromDb);

                }

                // Return token as response (e.g., in JSON format)
                HttpContext.Response.Cookies.Append("JwtToken", user, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(6)
                });

                string role = roles.ElementAt(0).ToString();
                if (role == "Admin")
                {

                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    if (UserFromDbByEmail != null)
                    {

                        return RedirectToAction("Index", "Profile", new { id = UserFromDbByEmail.UserId });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Profile", new { id = UserFromDb.UserId });

                    }

                }
            }

            ModelState.AddModelError(string.Empty, "Invalid username/email or password.");
        }

        return View(model);
    }
    // Example controller method for sign-out


    public IActionResult SignOut()
    {
        Response.Cookies.Delete("JwtToken");
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistartionViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.RegisterAsync(model);
            if (user != null)
            {
                if(user != "AlreadyExists")
                {

                return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User already Exists with this Username/Email");
                    return View(model);

                }
            }

            ModelState.AddModelError(string.Empty, "Failed to register user.");
        }

        return View(model);
    }
}
