using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class KategoriController : Controller
    {
        private readonly IKategoriService _kategoriService;

        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }

        public IActionResult Index()
        {
            var datas = _kategoriService.GetAllAsync().Result;
            return View(datas);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {
				if (_kategoriService.GetAllAsync().Result.Data.Any(x => x.Ad.ToLower() == model.Ad.ToLower()))
				{
					ModelState.AddModelError("", $"{model.Ad} zaten kullanılıyor.");
					return View(model);
				}
                Kategori category = new()
                {
                    Ad = model.Ad,
                    Durum = true,
                };

                var addedCategory = await _kategoriService.AddAsync(category);
                if (addedCategory.Succes)
                {
                    TempData["Message"] = addedCategory.Message;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Duzenle(int id)
        {
            var category = await _kategoriService.GetByIdAsync(id);
            ViewBag.Success = category.Succes;
            ViewBag.Message = category.Message;
            if (category.Succes)
            {
                CategoryAddModel model = new CategoryAddModel()
                {
                    Id = id,
                    Ad = category.Data.Ad
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Duzenle(CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {
                var category = await _kategoriService.GetByIdAsync(model.Id);
                category.Data.Ad = model.Ad;

                var addedCategory = await _kategoriService.UpdateAsync(category.Data);
                if (addedCategory.Succes)
                {
                    TempData["Message"] = addedCategory.Message;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Success = true;
            return View(model);
        }
        public async Task<IActionResult> Sil(int id)
        {
            var deletedEntity = await _kategoriService.MakeStatusPassive(id);
            if (deletedEntity.Succes)
            {
                TempData["Message"] = deletedEntity.Message;
                return RedirectToAction("Index");
            }
            TempData["Message"] = deletedEntity.Message;
            return RedirectToAction("Index");
        }
    }
}
