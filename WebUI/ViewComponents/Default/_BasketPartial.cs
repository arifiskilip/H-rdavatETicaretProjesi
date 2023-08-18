using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.ViewComponents.Default
{
    public class _BasketPartial : ViewComponent
    {
        private readonly ISepetDal _sepetDal;

        public _BasketPartial(ISepetDal sepetDal)
        {
            _sepetDal = sepetDal;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProducutListPageModel model = new ProducutListPageModel();
            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(TempData["UserId"]);
                var result = _sepetDal.GetItems(userId);
               
                if (result.Count>0)
                {
                    ViewBag.totalPrice = _sepetDal.CalculateTotal(userId);
                    model.Sepet = result;
                    return View(model);
                }
                return View(model);
            }
            return View(model);
        }

    }
}
