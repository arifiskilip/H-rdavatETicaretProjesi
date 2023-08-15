using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles ="Member")]
    public class SiparisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tamamlanmis()
        {
            return View();
        }
        public IActionResult IptalEdilmis()
        {
            return View();
        }
    }
}
