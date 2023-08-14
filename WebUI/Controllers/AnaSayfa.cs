using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    
    public class AnaSayfa : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
