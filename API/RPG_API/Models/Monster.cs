using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public enum TypeMonster
    {
        Easy,
        Medium,
        Hard,
        Boss
    }
    public class Monster: ModelBase
    {
        public TypeMonster Type { get; set; }
        public string Name { get; set; }
        public int XpGiven { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public ICollection<Quest> Quest { get; set; }
        public Map Map { get; set; }
    }
}
