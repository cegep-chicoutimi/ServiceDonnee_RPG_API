using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public enum TypeTile
    {
        Grass,
        Water,
        Sand,
        Forest,
        Mountain
    }

    public class Tile : ModelBase
    {
        public TypeTile Type { get; set; }
        public int Y {get; set; }
        public int X { get; set; }
        public int MapId { get; set; }
        public Map Map { get; set; }
    }
}
