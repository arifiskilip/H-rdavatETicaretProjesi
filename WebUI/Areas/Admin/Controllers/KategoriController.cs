using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                Kategori category = new()
                {
                    Ad = model.Ad,
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
            var deletedEntity = await _kategoriService.DeleteAsync(id);
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
