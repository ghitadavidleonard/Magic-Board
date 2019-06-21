using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicBoard
{
    public class BlessCard : Card
    {
        public BlessCard(string title, string description, int cardValues)
            : base(title, description, cardValues)
        {
        }

        public override void ApplyOn(Player player, int gameType)
        {
            player.UpdateLocation(new Square(player.GetPlayerLocation() + Value), gameType);
        }
    }
}
