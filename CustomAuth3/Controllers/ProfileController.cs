using AutoMapper;
using CustomAuth3.Database;
using CustomAuth3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomAuth3.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace CustomAuth3.Controllers
{
    [Authorize(Policy = "UserOrAdmin")]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;  

        public ProfileController(ApplicationDbContext context, IMapper mapper, IUserService userService) { 
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<IActionResult> Index(string? id)
        {
            var Userid = Int32.Parse(id);
           
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserId == Userid);
            if (user == null) {
                return BadRequest();
            }
            return View(user);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int currentUserId = GetCurrentUserId();
            if (currentUserId != id)
            {
                return Forbid();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EditUserViewModel>(user);
            return View(model);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUserViewModel model)
        {
            if (id != model.UserId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                try
                {
                    _mapper.Map(model, user);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    string UserId= id.ToString();
                    return RedirectToAction("Index", new {id=UserId});
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int currentUserId = GetCurrentUserId();
            if (currentUserId != id)
            {
                return Forbid();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            Response.Cookies.Delete("JwtToken");

            return Json(new { success = true });
        }

        public async Task<IActionResult> PasswordChange() { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel model)
        {

            bool flag = false;
            var UserFromDb= await  _context.Users.FindAsync(model.UserId);

            flag = _userService.VerifyPassword(model.CurrentPassword, UserFromDb.Password);

            if (flag)
            {
                UserFromDb.Password = _userService.HashPassword(model.NewPassword);
                _context.SaveChanges();
                return RedirectToAction("Index", new {id=model.UserId.ToString()});
            }
            else {

                ModelState.AddModelError(string.Empty, "Incorrect Password");
                return View();
            }


        }


        private int GetCurrentUserId()
        {
            string userId = null;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    userId = claim.Value;
                }
            }
            return int.Parse(userId);
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
