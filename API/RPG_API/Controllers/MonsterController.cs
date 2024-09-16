using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class MonsterController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
