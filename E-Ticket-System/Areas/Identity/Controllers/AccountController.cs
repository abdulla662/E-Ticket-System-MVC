using E_Ticket_System.Models;
using E_Ticket_System.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticket_System.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm registerVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationuser = new ApplicationUser()
                {
                    UserName = registerVM.Name,
                    Email = registerVM.Email,
                    Adress = registerVM.Adress,
                };
                var result = await _userManager.CreateAsync(applicationuser, registerVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
                ModelState.AddModelError("Password", "Not Match Constraints");
            }
            return View(registerVM);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var AppUser = await _userManager.FindByEmailAsync(loginVM.email);

                if (AppUser != null)
                {
                    var result = await _userManager.CheckPasswordAsync(AppUser, loginVM.password);
                    if (result)
                    {
                        await signInManager.SignInAsync(AppUser, loginVM.rememberme);
                        return RedirectToAction("Index", "Home", new { Area = "Customer" });
                    }
                    ModelState.AddModelError("password", "password is Not Found");

                }
                ModelState.AddModelError("Email", "Email is Not Found");
            }
            return View(loginVM);
        }
        public async Task<IActionResult> Logout(LoginVM login)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        public async Task<IActionResult> UserPage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UserProfileViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                Address = user.Adress,
                ProfilePicture = user.ProfilePicture,
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserPage(UserProfileViewModel model, IFormFile profileImage)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                user.UserName = model.Username;
                user.Email = model.Email;
                user.Adress = model.Address;
                user.ProfilePicture=model.ProfilePicture;
                if (profileImage != null && profileImage.Length > 0)
                {
                    var fileName = Path.GetFileName(profileImage.FileName);
                    var filePath = Path.Combine("wwwroot/images/profiles", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileImage.CopyToAsync(stream);
                    }

                    user.ProfilePicture = "/images/profiles/" + fileName;
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UserPage));
                }
            }

            return View(model);
        }
    }
}
