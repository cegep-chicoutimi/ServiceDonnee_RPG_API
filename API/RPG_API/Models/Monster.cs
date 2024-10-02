using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public enum DifficultyMonster
    {
        Easy,
        Medium,
        Hard,
        Boss
    }
    public enum Category
    {
        Spirit, 
        Demon, 
        Undead, 
        Giant, 
        Chimera, 
        Dragon, 
        Vampire, 
        Aquatic, 
        Flying, 
        Insect
    }
    public class Monster: ModelBase
    {
        public string Name { get; set; }
        public DifficultyMonster Difficulty { get; set; }
        public Category Category { get; set; }
        public int XpGiven { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public ICollection<Quest> Quest { get; set; }
        public int MapId { get; set; }

        public Map Map { get; set; }
    }
}
