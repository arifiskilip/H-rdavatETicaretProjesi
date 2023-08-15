using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarkaController : Controller
    {
        private readonly IMarkaService _markaService;

        public MarkaController(IMarkaService markaService)
        {
            _markaService = markaService;
        }

        public IActionResult Index()
        {
            var datas = _markaService.GetAllAsync().Result;
            return View(datas);
        }
        [HttpGet]
        public IActionResult Ekle()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(BrandAddModel model)
        {    
            if (ModelState.IsValid)
            {
                Marka marka = new()
                {
                    Ad = model.Ad,  
                };

                var addedBrand = await _markaService.AddAsync(marka);
                if (addedBrand.Succes)
                {
                    TempData["Message"] = addedBrand.Message;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Duzenle(int id)
        {
            var brand = await _markaService.GetByIdAsync(id);
            ViewBag.Success = brand.Succes;
            ViewBag.Message = brand.Message;
            if (brand.Succes)
            {
                BrandAddModel model = new BrandAddModel()
                {
                    Id = id,
                    Ad = brand.Data.Ad
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Duzenle(BrandAddModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = await _markaService.GetByIdAsync(model.Id);
                brand.Data.Ad = model.Ad;

                var addedProduct = await _markaService.UpdateAsync(brand.Data);
                if (addedProduct.Succes)
                {
                    TempData["Message"] = addedProduct.Message;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Success = true;
            return View(model);
        }
       
        public async Task<IActionResult> Sil(int id)
        {
            var deletedEntity = await _markaService.DeleteAsync(id);
            if (deletedEntity.Succes)
            {
                TempData["Message"]= deletedEntity.Message;
                return RedirectToAction("Index");
            }
            TempData["Message"] = deletedEntity.Message;
            return RedirectToAction("Index");
        }
    }
}
