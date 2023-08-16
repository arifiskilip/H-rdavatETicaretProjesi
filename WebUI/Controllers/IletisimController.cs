﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WebUI.Controllers
{
    public class IletisimController : Controller
    {
        [Authorize(Roles ="Üye")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                TempData["UserId"] = userId;
            }
            return View();
        }
    }
}
