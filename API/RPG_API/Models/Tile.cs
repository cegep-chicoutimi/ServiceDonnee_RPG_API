using RPG_API.Models.Base;

namespace RPG_API.Models
{
   
    public class Tile : ModelBase
    {
        public int Type { get; set; }
        public int Y {get; set; }
        public int X { get; set; }
    }
}
