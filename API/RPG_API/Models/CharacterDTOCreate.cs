namespace RPG_API.Models
{
    public class CharacterDTOCreate
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Lives { get; set; }
        public static CharacterDTOCreate CharacterToDTO(CharacterDTOCreate c)
        {
            return new CharacterDTOCreate { Name = c.Name, Damage = c.Damage, Armor = c.Armor, Lives = c.Lives};
        }
    }
}
