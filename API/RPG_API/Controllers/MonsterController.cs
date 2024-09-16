using Microsoft.AspNetCore.Mvc;
using RPG_API.Data.Context;

namespace RPG_API.Controllers
{
    public class MonsterController : ControllerBase
    {
        private readonly APIContext _context;

        public MonsterController(APIContext context)
        {
            _context = context;
        }

    }
}
