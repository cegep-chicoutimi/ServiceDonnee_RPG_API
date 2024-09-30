using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public enum TileType
    {
        Grass,
        Water,
        Sand,
        Forest,
        Mountain
    }

    public class Tile : ModelBase
    {
        public TileType Type { get; set; }
        public int Y {get; set; }
        public int X { get; set; }
        public int MapId { get; set; }
        public Map Map { get; set; }
    }
}
