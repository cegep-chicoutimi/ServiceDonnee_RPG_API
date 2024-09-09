using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
