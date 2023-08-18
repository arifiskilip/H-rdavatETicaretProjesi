using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles ="Member")]
    public class SiparisController : Controller
    {
        private readonly ISiparisService _siparisService;
        private readonly ISiparisDetayService _siparisDetayService;

        public SiparisController(ISiparisService siparisService, ISiparisDetayService siparisDetayService)
        {
            _siparisService = siparisService;
            _siparisDetayService = siparisDetayService;
        }

        public IActionResult Index()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = _siparisService.GetAllByCustomerId(userId);
            return View(result);
        }
        public IActionResult Detay(int id)
        {
            var result = _siparisDetayService.GetAllByOrderId(id);
            return View(result);
        }
        public IActionResult Tamamlanmis()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = _siparisService.GetAllByCompleted(userId);
            return View(result);
        }
        public IActionResult IptalEdilmis()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = _siparisService.GetAllByCanceled(userId);
            return View(result);
        }
    }
}
