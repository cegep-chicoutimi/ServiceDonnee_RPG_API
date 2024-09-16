using Microsoft.AspNetCore.Mvc;

namespace RPG_API.Controllers
{
    public class QuestController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
