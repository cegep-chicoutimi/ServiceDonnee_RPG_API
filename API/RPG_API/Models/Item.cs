namespace RPG_API.Models
{
    public enum TypeItem
    {
        Weapon,
        Armor,
        Consumable,
        Quest
    }
    public class Item
    {
        public string Name { get; set; }
        public double BoostAttack { get; set; }
        public double BoostDefence { get; set; }
        public int HealthRestoration { get; set; }
        public TypeItem Type { get; set; }
    }
}
