namespace RPG_API.Models
{
    public enum Type
    {
        Easy,
        Medium,
        Hard, 
        Boss
    }
    public class Tile
    {
        public Type Type { get; set; }
        public int Y {get; set; }
        public int X { get; set; }
    }
}
