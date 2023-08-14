using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.ViewComponents.Default
{
    public class _HomePageProductList : ViewComponent
    {
        private readonly IUrunService _urunService;

        public _HomePageProductList(IUrunService urunService)
        {
            _urunService = urunService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _urunService.GetAllAsync();
            return View(data.Data);
        }
    }
}
