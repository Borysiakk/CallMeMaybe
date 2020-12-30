using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CallMeMaybe.Web.Controllers
{
    public class AccountController :Controller
    {
        private readonly IIdentityService _identityService;
    
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _identityService.LoginAsync(model);
            if (result.Success)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error);
                }
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityService.RegisterAsync(model);
                
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error);
                    }
                }
            }

            return View(model);
        }
    }
}