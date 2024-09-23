namespace RPG_API.Models
{
    public class MonsterDTODetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DifficultyMonster Difficulty { get; set; }
        

        public static MonsterDTODetails MonsterToDTO(Monster m)
        {
            return new MonsterDTODetails { Id = m.Id, Name = m.Name, Difficulty = m.Difficulty };
        }
    }
}
