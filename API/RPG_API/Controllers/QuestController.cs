using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class QuestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
