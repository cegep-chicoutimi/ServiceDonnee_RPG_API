using RPG_API.Models.Base;

namespace RPG_API.Models
{
    public class JonctionItemCharacter : ModelBase
    {
        //Table jonction manuelle entre item et character
        int IdCharacter { get; set; }
        Character Character { get; set; }
        int IdItem { get; set; }
        Item Item { get; set; }
        bool isEquipped { get; set; }
    }
}
