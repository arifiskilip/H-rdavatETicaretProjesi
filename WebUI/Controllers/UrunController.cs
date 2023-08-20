using Business.Abstract;
using Business.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly ISepetDal _sepetDal;
        private readonly ISiparisService _siparisService;
        private readonly ISiparisDetayService _siparisDetayService;

        public UrunController(IMarkaService markaService, IUrunService urunService, IKategoriService kategoriService, ISepetDal sepetDal, ISiparisService siparisService, ISiparisDetayService siparisDetayService)
        {
            _markaService = markaService;
            _urunService = urunService;
            _kategoriService = kategoriService;
            _sepetDal = sepetDal;
            _siparisService = siparisService;
            _siparisDetayService = siparisDetayService;
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
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                TempData["UserId"] = userId;
            }
            ProducutListPageModel model = new()
            {
                UrunDto = _urunService.GetProductDto(id),
                UrunlerDto = _urunService.GetProductListDto()
            };

            return View(model);
        }
        [Authorize(Roles = "Member")]
        public IActionResult SiparisOlustur()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = _sepetDal.GetItems(userId);

            if (result.Count > 0)
            {
                ViewBag.totalPrice = _sepetDal.CalculateTotal(userId);
                return View(result);
            }
            return View();
        }
        public IActionResult UrunAra()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UrunAra(string searchTerm)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var searchResult = context.Urun.Where(u=> u.Ad.Contains(searchTerm) || context.Marka.Any(m=> m.Id == u.MarkaId && m.Ad.Contains(searchTerm)) ||
                context.Kategori.Any(k => k.Id == u.KategoriId && k.Ad.Contains(searchTerm))).ToList();
                IDataResult<List<Urun>> dataResult;
                if (searchResult.Count>0)
                {
                     dataResult = new SuccessDataResult<List<Urun>>(searchResult, "Listeleme işlemi başarılı.");
                }
                else
                {
                     dataResult = new ErrorDataResult<List<Urun>>(searchResult, "Listeleme işlemi başarısız.");
                }
                ProducutListPageModel model = new()
                {
                    BrandResult = await _markaService.GetAllAsync(),
                    CategoryResult = await _kategoriService.GetAllAsync(),
                    ProductResult = dataResult
                };
                return View(model);

            }
        }


        //jquery
        [HttpPost]
        public JsonResult Guncelle(int id, int adet)
        {
            try
            {
                using (HirdavatContext context = new HirdavatContext())
                {
                    if (adet >= 1)
                    {
                        var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                        var sepetUrun = context.Sepet.FirstOrDefault(x => x.UrunId == id && x.MusteriId == userId);
                        if (sepetUrun != null)
                        {
                            sepetUrun.Adet = adet;
                            context.SaveChanges();
                            return Json(new { success = true, message = "Güncelleme işlemi başarılı." });
                        }
                    }
                    return Json(new { success = false, message = "Lütfen adet miktarını kontrol edin." });
                }
            }
            catch (Exception)
            {

                return Json(new { success = true, message = "İşlem sırasında bir hata meydana geldi." });
            }
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> CreateOrderSendDb()
        {

            try
            {
                using (HirdavatContext context = new HirdavatContext())
                {
                    var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                    var baskets = _sepetDal.GetItems(userId);
                    var addedOrder = await context.Siparis.AddAsync(new()
                    {
                        CreateDate = DateTime.Now,
                        DurumId = (int)SiparisDurumEnum.Yolda,
                        KDVTutar = _sepetDal.CalculateKdv(userId),
                        MusteriId = userId,
                        Tarih = DateTime.Now,
                        ToplamFiyat = _sepetDal.CalculateTotal(userId),
                        FaturaTutari = _sepetDal.CalculateTotal(userId),
                        Indirim = 0,
                    });
                    await context.SaveChangesAsync();
                    foreach (var item in baskets)
                    {


                        if (addedOrder.Entity != null)
                        {

                            var addedOrderDetail = await _siparisDetayService.AddAsync(new()
                            {
                                Fiyat = _sepetDal.KdvAmount(item.UrunId)*(int)item.Adet,
                                SiparisId = addedOrder.Entity.Id,
                                TalepAdet = item.Adet,
                                UrunId = item.UrunId,
                            });
                        }
                    }
                    await context.SaveChangesAsync();
                    _sepetDal.Clear(userId);
                    return Json(new {success=true,message= "Siparişiniz başarıyla oluşturuldu!" });
                }
            }
            catch (Exception)
            {

                return Json(new { success = true, message = "Hata oluştu, lütfen tekrar deneyin." });
            }

        
        }

    }
}
