using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
