using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.ViewComponents.Default
{
    public class _SearchBoxPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
