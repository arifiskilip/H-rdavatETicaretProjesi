using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UrunController : Controller
    {
        private readonly IMarkaService _markaService;
        private readonly IUrunService _urunService;
        private readonly IKategoriService _kategoriService;

        public UrunController(IMarkaService markaService, IUrunService urunService, IKategoriService kategoriService)
        {
            _markaService = markaService;
            _urunService = urunService;
            _kategoriService = kategoriService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                TempData["UserId"] = userId;
            }

            ProducutListPageModel model = new()
            {
                BrandResult = await _markaService.GetAllAsync(),
                CategoryResult = await _kategoriService.GetAllAsync(),
                ProductResult = await _urunService.GetAllAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> Kategori(int id)
        {
            ProducutListPageModel model = new()
            {
                BrandResult = await _markaService.GetAllAsync(),
                CategoryResult = await _kategoriService.GetAllAsync(),
                ProductResult = await _urunService.ProductsWithCategoryIdGet(id)
            };
            return View(model);
        }
        public async Task<IActionResult> Marka(int id)
        {
            ProducutListPageModel model = new()
            {
                BrandResult = await _markaService.GetAllAsync(),
                CategoryResult = await _kategoriService.GetAllAsync(),
                ProductResult = await _urunService.ProductsWithBrandIdGet(id)
            };
            return View(model);
        }

        public IActionResult Detay(int id)
        {
            ProductDetailPageModel model = new()
            {
                UrunDto = _urunService.GetProductDto(id),
                UrunlerDto = _urunService.GetProductListDto()
            };
                      
            return View(model);
        }
    }
}
