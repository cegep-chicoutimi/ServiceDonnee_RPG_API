using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public class Class: ModelBase
    {
        public string Name { get; set; }
        public double BoostAttack { get; set; }
        public double BoostDefence { get; set; }

    }
}
