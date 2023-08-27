using Business.Abstract;
using Business.Utilities.Helpers;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MusteriController : Controller
    {
        private readonly IMusteriDal _musteriDal;
        private readonly IMusteriService _musteriService;
        private readonly ISiparisService _siparisService;

        public MusteriController(IMusteriDal musteriDal, ISiparisService siparisService, IMusteriService musteriService)
        {
            _musteriDal = musteriDal;
            _siparisService = siparisService;
            _musteriService = musteriService;
        }


        public IActionResult Index()
        {
            var result = _musteriDal.GetAllCustomers();
            return View(result);
        }

        public IActionResult SiparisDetay(int id)
        {
            var result = _siparisService.GetAllByCustomerId(id);
            return View(result);
        }



        //Ajax
        //İndirim ekle
        public async Task<IActionResult> DiscountAdd(int userId,int discount)
        {
            var checkUser = await _musteriService.GetByIdAsync(userId);
            if (checkUser.Succes)
            {
                checkUser.Data.IndirimOrani = discount;
                var result = await _musteriService.UpdateAsync(checkUser.Data);
                if (result.Succes)
                {
                    return Json(new { success = result.Succes, message = result.Message });
                }
                return Json(new { success = result.Succes, message = result.Message });
            }
            return Json(new { success = checkUser.Succes, message = checkUser.Message });
        }
        //Müşteri Sil
        public async Task<IActionResult> DeleteUser (int userId)
        {
            var checkUser = await _musteriService.GetByIdAsync(userId);
            if (checkUser.Succes)
            {
                var result = await _musteriService.MakeStatusPassive(userId);
                if (result.Succes)
                {
                    return Json(new { success = result.Succes, message = result.Message });
                }
                return Json(new { success = result.Succes, message = result.Message });
            }
            return Json(new { success = checkUser.Succes, message = checkUser.Message });
        }
        //Active User
        public async Task<IActionResult> ActiveUser(int userId)
        {
            var checkUser = await _musteriService.GetByIdAsync(userId);
            if (checkUser.Succes)
            {
                var result = await _musteriService.MakeStatusActive(userId);
                if (result.Succes)
                {
                    return Json(new { success = result.Succes, message = result.Message });
                }
                return Json(new { success = result.Succes, message = result.Message });
            }
            return Json(new { success = checkUser.Succes, message = checkUser.Message });
        }

        //Musteri Sifre Degistir
        public async Task<IActionResult> ChangePassword(int userId, string password)
        {
            var checkUser = await _musteriService.GetByIdAsync(userId);
            if (checkUser.Succes)
            {   
                if (password == null)
                {
                    return Json(new { success = false, message = "Şifre alanı boş olamaz." });
                }
                if (password.Length>= 5 && password.Length<=30)
                {
                    checkUser.Data.Sifre = password;
                    var result = await _musteriService.UpdateAsync(checkUser.Data);
                    if (result.Succes)
                    {
                        return Json(new { success = result.Succes, message = result.Message });
                    }
                    return Json(new { success = result.Succes, message = result.Message });
                }
            }
            return Json(new { success = false, message = "Günceleme işlemi başarısız." });
        }
    }
}
