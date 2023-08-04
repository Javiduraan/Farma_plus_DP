using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Farma_plus.Models;
using System.Security.Claims;
using Farma_plus.Interfaces;
using System.Threading.Tasks;

public class LoginController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        
        var user = await _unitOfWork.Users.GetUserByIdAsync(model.Usuario, model.Contraseña);

        if (user != null)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Usuario o Contraseña incorrecta.");
        return View(model);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Logout()
    {
        //_signInManager.Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}