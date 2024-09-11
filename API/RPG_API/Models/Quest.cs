using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public class Quest : ModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Reward { get; set; }
        public bool Status { get; set; }
        public Character Character { get; set; }
        public Monster? Monster { get; set; }

       
    }
}
