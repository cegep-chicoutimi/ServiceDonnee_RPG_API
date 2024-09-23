using RPG_API.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPG_API.Models
{
    public class Character : ModelBase
    {

        public string Name { get; set; }
        // Inventaire d'items du personnage
        public ICollection<Item>? Inventory { get; set; }
        // Items équipés
        public ICollection<JonctionItemCharacter>? Equipment { get; set; }
        public int Xp { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Lives { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public List<Quest>? Quests { get; set; }
        public Map? Map { get; set; }
        

    }
}
