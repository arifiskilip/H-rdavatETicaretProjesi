using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Threading.Tasks;
using DataAccess.Contexts;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using DataAccess.Helpers;
using System.Linq;
using System;
using Microsoft.VisualBasic;
using Entities.Enums;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SiparisController : Controller
    {
        private readonly ISiparisService _siparisService;
        private readonly ISiparisDetayService _siparisDetayService;
        private readonly IUrunService _urunService;

        public SiparisController(ISiparisService siparisService, ISiparisDetayService siparisDetayService, IUrunService urunService)
        {
            _siparisService = siparisService;
            _siparisDetayService = siparisDetayService;
            _urunService = urunService;
        }

        public IActionResult Index()
        {
            var result = _siparisService.GetAllByOrderBy();
            return View(result);
        }
        public IActionResult Detay(int id)
        {
            var result = _siparisDetayService.GetAllByOrderId(id);
            return View(result);
        }
        public IActionResult TamamlananSiparisler()
        {
            var result = _siparisService.GetAllByCompleted();
            return View(result);
        }
        public IActionResult IptalEdilenSiparisler()
        {
            var result = _siparisService.GetAllByCanceled();
            return View(result);
        }





        //Ajax

        //Yeni Teslimat Miktarı
        [HttpPost]
        public async Task<ActionResult> EditNewDeliveryAmount(int siparisDetayId, int yeniTeslimAdet)
        {
            try
            {
                var orderDetail = await _siparisDetayService.GetByIdAsync(siparisDetayId);
                if (orderDetail.Succes)
                {
                    var stockControl = await _urunService.GetByIdAsync((int)orderDetail.Data.UrunId);
                    if (stockControl != null)
                    {
                        if (yeniTeslimAdet < 1)
                        {
                            return Json(new { success = false, message = "Teslim adeti 1 den küçük olamaz." });
                        }
                        if (stockControl.Data.KutuAdet >= yeniTeslimAdet && orderDetail.Data.TeslimAdet == null)
                        {
                            stockControl.Data.KutuAdet -= yeniTeslimAdet;
                        }
                        else if (stockControl.Data.KutuAdet >= yeniTeslimAdet && orderDetail.Data.TeslimAdet != null)
                        {
                            if (orderDetail.Data.TeslimAdet > yeniTeslimAdet)
                            {
                                int adet = (int)orderDetail.Data.TeslimAdet - yeniTeslimAdet;
                                stockControl.Data.KutuAdet += adet;

                            }
                            else if (orderDetail.Data.TeslimAdet < yeniTeslimAdet)
                            {
                                int adet = yeniTeslimAdet - (int)orderDetail.Data.TeslimAdet;
                                stockControl.Data.KutuAdet -= adet;

                            }
                            else
                            {
                                return Json(new { success = true, message = "Güncelleme işlemi başarılı." });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = $"İlgili üründen yanlızca {stockControl.Data.KutuAdet} adet kaldı. Lütfen stok adeti kadar miktar giriniz." });
                        }

                        await _urunService.UpdateAsync(stockControl.Data);
                        orderDetail.Data.TeslimAdet = yeniTeslimAdet;
                        orderDetail.Data.Fiyat = CalculateHalpers.KdvAmount(stockControl.Data.Id, yeniTeslimAdet);
                        await _siparisDetayService.UpdateAsync(orderDetail.Data);
                        var order = await _siparisService.GetByIdAsync((int)orderDetail.Data.SiparisId);
                        var orderDetailByAllProduct = _siparisDetayService.GetAllAsync().Result.Data.Where(x => x.SiparisId == (int)order.Data.Id);
                        double totalPrice = 0;
                        foreach (var item in orderDetailByAllProduct)
                        {
                            totalPrice += (double)item.Fiyat;
                        }
                        order.Data.ToplamFiyat = totalPrice;
                        order.Data.FaturaTutari = totalPrice;
                        await _siparisService.UpdateAsync(order.Data);
                        return Json(new { success = true, message = "Güncelleme işlemi başarılı." });

                    }
                    else
                    {
                        return Json(new { success = false, message = stockControl.Message });
                    }

                }

                return Json(new
                {
                    success = false,
                    message = "Güncelleme işlemi başarısız."
                });
            }
            catch (System.Exception)
            {

                return Json(new
                {
                    success = false,
                    message = "Güncelleme işlemi başarısız."
                });
            }
        }

        //Siparis icin indirim ekleme.
        [HttpPost]
        public async Task<IActionResult> AddDiscountToOrder(int orderId, int discount)
        {
            try
            {
                var order = await _siparisService.GetByIdAsync(orderId);
                if (order.Succes)
                {
                    if (discount >= 1)
                    {
                        double totalPrice = (double)order.Data.ToplamFiyat;
                        totalPrice = Math.Round(totalPrice - (totalPrice * discount / 100),2);
                        order.Data.ToplamFiyat = totalPrice;
                        order.Data.FaturaTutari = totalPrice;
                        order.Data.Indirim = discount;
                        await _siparisService.UpdateAsync(order.Data);
                        return Json(new { success = true, message = "İndirim işlemi başarıyla gerçekleşti." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "İndirim miktarı 0'dan küçük olamaz." });
                    }
                }
                return Json(new { success = order.Succes, message = order.Message });
            }
            catch (System.Exception)
            {

                return Json(new { success = false, message = "Lütfen geçerli bir indirim değeri giriniz." });
            }
        }

        //İndirim İptali
        [HttpPost]
        public async Task<IActionResult> DiscountCanceled(int orderId)
        {
            try
            {
                var orderDetail = _siparisDetayService.GetAllByOrderId(orderId);
                double totalPrice = 0;

                foreach (var item in orderDetail.Data)
                {
                    if (item.TeslimAdet!=null)
                    {
                        totalPrice +=CalculateHalpers.KdvAmount((int)item.UrunId, (int)item.TeslimAdet);
                    }
                    else
                    {
                        totalPrice += CalculateHalpers.KdvAmount((int)item.UrunId, (int)item.TalepAdet);
                    }
                    
                }
                var order = await _siparisService.GetByIdAsync(orderId);
                order.Data.ToplamFiyat = totalPrice;
                order.Data.FaturaTutari = totalPrice;
                order.Data.Indirim = 0;
                await _siparisService.UpdateAsync(order.Data);
                return Json(new { success = true, message = "İndirim işlemi başarıyla iptal edildi." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "İşlem başarısız." });
                
            }
        }
        //Sipariş Onayla
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int orderId)
        {
            try
            {
                var order =await _siparisService.GetByIdAsync(orderId);
                if (order.Succes)
                {
                    order.Data.DurumId = (int)SiparisDurumEnum.Tamamlandi;
                    await _siparisService.UpdateAsync(order.Data);
                    return Json(new { success = true, message = "Siparişiniz tamamlandı!" });
                }
                return Json(new { success = false, message = order.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "İşlem sırasında bir hata meydana geldi!" });
                
            }
        }
        // Sipariş iptal et
        [HttpPost]
        public async Task<IActionResult> CanceledOrder(int orderId)
        {
            try
            {
                var order = await _siparisService.GetByIdAsync(orderId);
                if (order.Succes)
                {
                    order.Data.DurumId = (int)SiparisDurumEnum.İptalEdildi;
                    await _siparisService.UpdateAsync(order.Data);
                    return Json(new { success = true, message = "Siparişiniz iptal edildi!" });
                }
                return Json(new { success = false, message = order.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "İşlem sırasında bir hata meydana geldi!" });

            }
        }

    }


}

