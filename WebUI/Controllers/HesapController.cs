using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using WebUI.Models;
using System.Threading.Tasks;
using System.Linq;
using DataAccess.Contexts;
using System;
using Entities.Enums;

namespace WebUI.Controllers
{
    public class HesapController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IRolService _rolService;
        private readonly IMusteriService _musteriService;
        private readonly IMusteriRolService _musteriRolService;

        public HesapController(IAuthService authService, IRolService rolService, IMusteriService musteriService, IMusteriRolService musteriRolService)
        {
            _authService = authService;
            _rolService = rolService;
            _musteriService = musteriService;
            _musteriRolService = musteriRolService;
        }
        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Giris(LoginModel model)
        {
            var claims = new List<Claim>();
            if (ModelState.IsValid)
            {
                Musteri musteri = new()
                {
                    Email = model.Email,
                    Sifre = model.Password
                };

               var result = _authService.Login(musteri);
                if (result.Succes)
                {
                    var roles = await _rolService.GetUserRolesAsync(result.Data.Id);
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Ad));
                    }
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index", "AnaSayfa");

            }
            return View(model);
        }
        public IActionResult Kaydol()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Kaydol(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                Musteri musteri = new()
                {
                    Ad = model.FirstName,
                    Soyad = model.LastName,
                    Email = model.Email,
                    Sifre = model.Password
                };
                var emailExist = _authService.UserExists(model.Email);
                if (emailExist.Succes)
                {
                    using (HirdavatContext context = new HirdavatContext())
                    {
                        var addedEntity = await context.Musteri.AddAsync(musteri);
                        await context.SaveChangesAsync();
                        MusteriRol customerRol = new()
                        {
                            MusteriId = addedEntity.Entity.Id,
                            RoleId = Convert.ToInt16(RolEnum.Üye)
                        };
                        await context.MusteriRol.AddAsync(customerRol);
                        await context.SaveChangesAsync();
                        return RedirectToAction("Giris");
                    }
                }
                ModelState.AddModelError("", emailExist.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("AnaSayfa", "Home");
        }
    }
}
