using Microsoft.EntityFrameworkCore;
using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public class Map : ModelBase
    {
        public ICollection<Tile> Coordinates { get; set; }
        public string ImageUrl { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public ICollection<Monster?> Monster { get; set; }
    }
}
