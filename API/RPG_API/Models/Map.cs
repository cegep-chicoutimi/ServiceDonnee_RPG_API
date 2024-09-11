using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public class Map : ModelBase
    {
        public List<List<int>> Coordinates { get; set; }
        public string ImageUrl { get; set; }
        public Character Character { get; set; }    
        public List<Monster> Monster { get; set; }


        
    }
}
