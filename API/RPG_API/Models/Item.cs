using System.ComponentModel.DataAnnotations.Schema;

namespace RPG_API.Models.Base
{
    public enum TypeItem
    {
        Weapon,
        Armor,
        Consumable,
        Quest
    }
    public class Item : ModelBase
    {
        public string Name { get; set; }
        public double BoostAttack { get; set; }
        public double BoostDefence { get; set; }
        public int HealthRestoration { get; set; }
        public TypeItem Type { get; set; }
        public ICollection<Character?> Characters { get; set; }
    }
}
