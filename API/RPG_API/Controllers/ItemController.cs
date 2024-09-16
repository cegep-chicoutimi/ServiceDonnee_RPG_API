using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class ItemController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
