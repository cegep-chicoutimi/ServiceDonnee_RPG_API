namespace RPG_API.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Inventory { get; set; }
        public List<int> Equipement { get; set; }
        public int Xp { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Lives { get; set; }
    }
}
