using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class MonsterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
