using Business.Abstract;
using Business.Utilities.Helpers;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            ViewBag.productId = id;
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
            var deletedEntity = await _urunService.DeleteAsync(id);
            DeleteAllProductImagesByProductId(id);
            if (deletedEntity.Succes)
            {
                TempData["Message"] = deletedEntity.Message;
                return RedirectToAction("Index");
            }
            TempData["Message"] = deletedEntity.Message;
            return RedirectToAction("Index");
        }

        public IActionResult ResimEkle(int id)
        {
            ViewBag.productId = id;
            using (HirdavatContext context = new HirdavatContext())
            {
                
                var prdocutImages = context.UrunResim.Include(x=> x.Urun).Where(x=> x.UrunId == id).ToList();
                return View(prdocutImages);
            }
          
        }




        //jquery
        public async Task<IActionResult> AddImage(IFormFile file, int productId)
        {

            using (HirdavatContext context = new HirdavatContext())
            {
                var imageResult = FileHelper.Add(file);
                if (imageResult.Succes)
                {
                    var imageCount = context.UrunResim.Where(x => x.UrunId == productId).Count();
                    if (imageCount<= 5)
                    {
                        var addedImage = await context.UrunResim.AddAsync(new()
                        {
                            ResimYol = imageResult.Message,
                            UrunId = productId
                        });
                        await context.SaveChangesAsync();
                        return Json(new { success = imageResult.Succes, message = "Resim başarıyla eklendi." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Maksimum resim sayısına ulaştınız. Lütfen yeni bir resim için var olan resimlerden silme işlemi gerçekleştirin." });
                    }
                   
                   
                }
                return Json(new { success = imageResult.Succes, message = imageResult.Message });
            }
            
        }

        [HttpPost]
        public IActionResult DeleteImage(int productImageId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var checkImage = context.UrunResim.Find(productImageId);
                if (checkImage != null)
                {
                    FileHelper.Delete(checkImage.ResimYol);
                    context.UrunResim.Remove(checkImage);
                    context.SaveChanges();

                    return Json(new { success = true, message = "Silme işlemi başarılı!" });
                }
            }
            return Json(new { success = false, message = "Silme işlemi sırasında hata meydana geldi!" });
        }

        private void DeleteAllProductImagesByProductId(int productId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var result = context.UrunResim.Where(x => x.UrunId == productId).ToList();
                foreach (var item in result)
                {
                    FileHelper.Delete(item.ResimYol);
                    context.UrunResim.Remove(item);
                }
                context.SaveChanges();
            }
        }
    }
   
}
