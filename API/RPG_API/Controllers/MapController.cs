using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class MapController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
