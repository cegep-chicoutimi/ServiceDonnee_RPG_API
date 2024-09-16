using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
