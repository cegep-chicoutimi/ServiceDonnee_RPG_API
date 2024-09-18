namespace RPG_API.Models
{
    public class QuestDTOCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Reward { get; set; }
        public bool Status { get; set; }

        public static QuestDTOCreate QuestToDTO(QuestDTOCreate q)
        {
            return new QuestDTOCreate { Title = q.Title, Description = q.Description, Reward = q.Reward, Status = q.Status};
        }
    }
}
