using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.Claims;
using Electronics_Shop.Properties;
using Electronics_Shop.Models;

namespace Electronics_Shop.Controllers
{
    public class AccountController : Controller
    {
        private readonly ConfigrationSettings _config;

        public AccountController(ConfigrationSettings config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.AdminUN = _config.admin_username;
            ViewBag.AdminPass = _config.admin_password;

            ViewBag.CustUN = _config.customer_username;
            ViewBag.CustPass = _config.customer_password;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userOK = AuthenticateUser(model.UserName.ToUpper().Trim(), model.Password, out bool adminAccount);

                if (userOK)
                {

                    var claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, model.UserName),
                            new Claim(ClaimTypes.Role, adminAccount?"Admin":"Customer"),
                            //new Claim("Status", user.Status)
                        };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                                           CookieAuthenticationDefaults.AuthenticationScheme,
                                           new ClaimsPrincipal(claimsIdentity),
                                           new AuthenticationProperties
                                           {
                                               IsPersistent = model.RememberMe
                                           });

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError("", "Invalid Login Attempt");
            }

            return View(model);
        }

        private bool AuthenticateUser(string login, string password, out bool adminRole)
        {
            adminRole = false;

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                if (login.ToUpper() == _config.admin_username.ToUpper() && password == _config.admin_password)
                {
                    adminRole = true;
                    return true;
                }
                else if (login.ToUpper() == _config.customer_username.ToUpper() && password == _config.customer_password)
                    return true;
                else
                    return false;
            }

            return false;
        }
    }
}
