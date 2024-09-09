namespace RPG_API.Models
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XpGiven { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
    }
}
