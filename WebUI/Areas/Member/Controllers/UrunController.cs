using Business.Abstract;
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
        [Authorize(Roles = "Member")]
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
        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> UrunAdetEkle(int id,int quantity)
        {
            var checkProduct = await _urunService.GetByIdAsync(id);
            if (checkProduct.Succes)
            {
                if (quantity>0)
                {
                    var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                    _sepetDal.AddItem(id, quantity, userId);
                    return Json(new { success = true, message = "Ürün sepete başarıyla eklendi." });
                }
                else
                {
                    return Json(new { success = false, message = "Ürün adeti 0'dan büyük olmalıdır." });
                }
            }
            return Json(new { success = false, message = "Ürün sepete eklenmedi." });
        }
        [Authorize(Roles = "Member")]
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
