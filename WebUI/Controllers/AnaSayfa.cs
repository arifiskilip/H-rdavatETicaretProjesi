using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace WebUI.Controllers
{
    
    public class AnaSayfa : Controller
    {
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
