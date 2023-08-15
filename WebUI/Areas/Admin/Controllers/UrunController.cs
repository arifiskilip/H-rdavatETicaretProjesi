using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;
        private readonly IMarkaService _markaService;
        private readonly IKategoriService _kategoriService;

        public UrunController(IUrunService urunService, IMarkaService markaService, IKategoriService kategoriService)
        {
            _urunService = urunService;
            _markaService = markaService;
            _kategoriService = kategoriService;
        }

        public IActionResult Index()
        {
            var datas = _urunService.GetProductListDto();
            return View(datas);
        }
        [HttpGet]
        public async Task<IActionResult> Ekle()
        {
            var brands = await _markaService.GetAllAsync();
            var categories = await _kategoriService.GetAllAsync();
            ProductAddModel model = new ProductAddModel()
            {
                Markalar = new SelectList(brands.Data, "Id", "Ad"),
                Kategoriler = new SelectList(categories.Data, "Id", "Ad")
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(ProductAddModel model)
        {
            var brands = await _markaService.GetAllAsync();
            var categories = await _kategoriService.GetAllAsync();
            model.Markalar = new SelectList(brands.Data, "Id", "Ad");
            model.Kategoriler = new SelectList(categories.Data, "Id", "Ad");
            if (ModelState.IsValid)
            {
                Urun urun = new()
                {
                    Ad = model.Ad,
                    Iskonto1 = model.Iskonto1,
                    Iskonto2 = model.Iskonto2,
                    Iskonto3 = model.Iskonto3,
                    KategoriId = model.KategoriId,
                    Kod = model.Kod,
                    KutuAdet = model.KutuAdet,
                    ListeFiyat = model.ListeFiyat,
                    MarkaId = model.MarkaId,
                    StokDurum = model.StokDurum
                };

                var addedProduct = await _urunService.AddAsync(urun);
                if (addedProduct.Succes)
                {
                    TempData["Message"] = addedProduct.Message;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Duzenle(int id)
        {
            var product = await _urunService.GetByIdAsync(id);
            ViewBag.Success = product.Succes;
            ViewBag.Message = product.Message;
            if (product.Succes)
            {
                var brands = await _markaService.GetAllAsync();
                var categories = await _kategoriService.GetAllAsync();
                ProductAddModel model = new ProductAddModel()
                {
                    Id = id,
                    Ad = product.Data.Ad,
                    KutuAdet = product.Data.KutuAdet,
                    Iskonto1 = product.Data.Iskonto1,
                    Iskonto2 = product.Data.Iskonto2,
                    Iskonto3 = product.Data.Iskonto3,
                    KategoriId = product.Data.KategoriId,
                    MarkaId = product.Data.MarkaId,
                    Kod = product.Data.Kod,
                    ListeFiyat = product.Data.ListeFiyat,
                    StokDurum = product.Data.StokDurum,
                    Markalar = new SelectList(brands.Data, "Id", "Ad"),
                    Kategoriler = new SelectList(categories.Data, "Id", "Ad")
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Duzenle(ProductAddModel model)
        {
            var brands = await _markaService.GetAllAsync();
            var categories = await _kategoriService.GetAllAsync();
            model.Markalar = new SelectList(brands.Data, "Id", "Ad");
            model.Kategoriler = new SelectList(categories.Data, "Id", "Ad");
            if (ModelState.IsValid)
            {
                var urun = await _urunService.GetByIdAsync(model.Id);
                urun.Data.Ad = model.Ad;
                urun.Data.Iskonto1 = model.Iskonto1;
                urun.Data.Iskonto2 = model.Iskonto2;
                urun.Data.Iskonto3 = model.Iskonto3;
                urun.Data.KategoriId = model.KategoriId;
                urun.Data.Kod = model.Kod;
                urun.Data.KutuAdet = model.KutuAdet;
                urun.Data.ListeFiyat = model.ListeFiyat;
                urun.Data.MarkaId = model.MarkaId;
                urun.Data.StokDurum = model.StokDurum;
                urun.Data.CreateDate = System.DateTime.Now;

                var addedProduct = await _urunService.UpdateAsync(urun.Data);
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
                TempData["Message"] = deletedEntity.Message;
                return RedirectToAction("Index");
            }
            TempData["Message"] = deletedEntity.Message;
            return RedirectToAction("Index");
        }

    }
}
