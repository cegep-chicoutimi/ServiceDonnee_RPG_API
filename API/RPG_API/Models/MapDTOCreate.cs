namespace RPG_API.Models
{
    public class MapDTOCreate
    {
        public ICollection<Tile> Coordinates { get; set; }
        public string ImageUrl { get; set; }
       
        public static MapDTOCreate MapToDTO(MapDTOCreate ma)
        {
            return new MapDTOCreate { Coordinates = ma.Coordinates, ImageUrl = ma.ImageUrl };

        }
    }
}
