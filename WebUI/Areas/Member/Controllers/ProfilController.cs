using Business.Abstract;
using Business.Utilities.Helpers;
using Business.Utilities.Results;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;
using WebUI.Areas.Member.Models;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    public class ProfilController : Controller
    {
        private readonly IIlceService _ılceService;
        private readonly IIlService _ılService;
        public readonly IMusteriService _musteriService;
        private readonly IAuthService _authService;

        public ProfilController(IIlceService ılceService, IIlService ılService, IMusteriService musteriService, IAuthService authService)
        {
            _ılceService = ılceService;
            _ılService = ılService;
            _musteriService = musteriService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var citys = await _ılService.GetAllAsync();
            var ılceler = await _ılceService.GetAllAsync();
            ViewBag.Iller = new SelectList(citys.Data, "Id", "Ad");
            ViewBag.Ilceler = new SelectList(ılceler.Data, "Id", "Ad");
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var getUser = await _musteriService.GetByIdAsync(userId);
            MemberUpdateModel model = new MemberUpdateModel()
            {
                Ad = getUser.Data.Ad,
                Soyad = getUser.Data.Soyad,
                Adres = getUser.Data.Adres,
                FirmaAd = getUser.Data.FirmaAd,
                Id = getUser.Data.Id,
                IlceId = getUser.Data.IlceId,
                IlId = getUser.Data.IlId,
                Telefon = getUser.Data.Telefon,
                MevcutResim = getUser.Data.Resim
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(MemberUpdateModel model)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var getUser = await _musteriService.GetByIdAsync(userId);
            var citys = await _ılService.GetAllAsync();
            ViewBag.Iller = new SelectList(citys.Data, "Id", "Ad");
            if (ModelState.IsValid)
            {
                if (model.Resim != null)
                {
                   var result = FileHelper.Add(model.Resim);
                    if (!result.Succes)
                    {
                        TempData["Message"] = result.Message;
                        ViewBag.Success = false;
                        return View(model);
                    }
                    getUser.Data.Resim = result.Message;
                }
                getUser.Data.Ad = model.Ad;
                getUser.Data.Soyad = model.Soyad;
                getUser.Data.Adres = model.Adres;
                getUser.Data.FirmaAd = model.FirmaAd;
                if (model.IlceId != null)
                {
                    getUser.Data.IlceId = model.IlceId;
                }
                getUser.Data.IlId = model.IlId;
                var checkPhone = _authService.UserExists(model.Telefon);
                if (getUser.Data.Telefon == model.Telefon)
                {
                    getUser.Data.Telefon = model.Telefon;
                }
                else if (checkPhone.Succes)
                {
                    getUser.Data.Telefon = model.Telefon;
                }
                else
                {
                    ModelState.AddModelError("", checkPhone.Message); 
                    return View(model);
                }
                

                var updatedUser = await _musteriService.UpdateAsync(getUser.Data);
                if (!updatedUser.Succes)
                {
                    ViewBag.Success = false;
                    TempData["Message"] = updatedUser.Message;
                }
                TempData["Message"] = updatedUser.Message;
                ViewBag.Success = true;
                return View();
            }
            
            return View(model);
        }
        [HttpGet]
        public JsonResult GetIlceler(int ilId)
        {
            using (HirdavatContext context = new HirdavatContext())
            {
                var ilceList = context.Ilce.ToList().Where(ilce => ilce.IlId == ilId).ToList();
                return Json(ilceList);
            }
        }
        [HttpGet]
        public IActionResult Guvenlik()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Guvenlik(MemberChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                var getUser = await _musteriService.GetByIdAsync(userId);
                if (getUser.Data.Sifre == model.LastPassword)
                {
                    getUser.Data.Sifre = model.PasswordConfirm;
                    await _musteriService.UpdateAsync(getUser.Data);
                    TempData["Message"] = getUser.Message;
                }
            }
            return View();
        }
    }
}
