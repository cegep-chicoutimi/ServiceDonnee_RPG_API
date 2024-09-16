namespace RPG_API.Models
{
    public class CharacterDTOLogin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Xp { get; set; }
        public static CharacterDTOLogin CharacterToDTO(CharacterDTOLogin c)
        {
            return new CharacterDTOLogin { Id = c.Id, Name = c.Name, Xp = c.Xp };
        }

    }
}
