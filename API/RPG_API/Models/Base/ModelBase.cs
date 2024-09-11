namespace RPG_API.Models.Base
{
    public abstract class ModelBase
    {
        public int Id { get; set; }
        //Pour la sérialisation
        public ModelBase()
        {
        }
        public ModelBase(int id)
        {
            Id = id;
        }
    }
}
