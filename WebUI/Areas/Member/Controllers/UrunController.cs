﻿using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    public class UrunController : Controller
    {
        
        private readonly IUrunService _urunService;
        private readonly ISepetDal _sepetDal;

        public UrunController(IUrunService urunService, ISepetDal sepetDal)
        {
            _urunService = urunService;
            _sepetDal = sepetDal;
        }

        public async Task<IActionResult> Ekle(int id)
        {
            var checkProduct =await _urunService.GetByIdAsync(id);
            if (checkProduct.Succes)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                _sepetDal.AddItem(id, 1,userId);
            }
            return Redirect("/AnaSayfa");
        }

        public async Task<IActionResult> Sil(int id)
        {
            var checkProduct = await _urunService.GetByIdAsync(id);
            if (checkProduct.Succes)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                _sepetDal.RemoveItem(id,userId);
            }
            return Redirect("/AnaSayfa");
        }
    }
}
