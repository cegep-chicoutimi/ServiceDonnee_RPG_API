using Microsoft.AspNetCore.Mvc;
using RPG_API.Data.Context;

namespace RPG_API.Controllers
{
    public class QuestController : ControllerBase
    {
        private readonly APIContext _context;

        public QuestController(APIContext context)
        {
            _context = context;
        }

    }
}
