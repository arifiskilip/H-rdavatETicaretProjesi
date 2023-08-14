using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        public IActionResult Index()
        {
            var datas = _urunService.GetProductListDto();
            return View(datas);
        }

        public IActionResult Ekle()
        {
            return View();
        }
    }
}
