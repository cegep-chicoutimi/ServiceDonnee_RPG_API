using Microsoft.AspNetCore.Mvc;
using RPG_API.Data.Context;

namespace RPG_API.Controllers
{
    public class TileController : ControllerBase
    {
        private readonly APIContext _context;

        public TileController(APIContext context)
        {
            _context = context;
        }

    }
}
