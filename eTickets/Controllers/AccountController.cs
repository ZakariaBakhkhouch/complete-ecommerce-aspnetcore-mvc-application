using Couchbase.Query;
using eTickets.Data;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        [HttpGet]
        public IActionResult Login()
        {
            var response = new UserLogin();
            return View(response);
        }
        

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                ModelState.AddModelError("", "This account is not exist, Create one to Login.");
                return View();
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

            if(passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Movies");
                }
            }

            ModelState.AddModelError("", "Password is incorrect");

            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            var response = new User();
            return View(response);
        }


        [HttpGet]
        public IActionResult RegisterCompleted()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user != null) ModelState.AddModelError("", "This email is alraedy in use");

            ApplicationUser newUser = new()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.FirstName + " " + model.LastName,
            };

            var userResponse = await _userManager.CreateAsync(newUser, model.Password);

            if (userResponse.Succeeded)
            {
                var role = await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else
            {
                foreach (var error in userResponse.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            return View(nameof(RegisterCompleted));
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
    }
}
