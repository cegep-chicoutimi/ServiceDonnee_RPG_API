namespace RPG_API.Models
{
    public class MonsterDTOCreate
    {
        public TypeMonster Type { get; set; }
        public string Name { get; set; }
        public int XpGiven { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }

        public static MonsterDTOCreate MonsterToDTO(MonsterDTOCreate m)
        {
            return new MonsterDTOCreate { Type = m.Type, Name = m.Name, XpGiven = m.XpGiven, Damage = m.Damage, Armor = m.Armor, Health = m.Health };
        }
    }
}
