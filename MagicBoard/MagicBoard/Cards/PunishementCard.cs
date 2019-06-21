namespace MagicBoard
{
    public class PunishementCard : Card
    {
        public PunishementCard(string title, string description, int cardValues) : base(title, description, cardValues)
        {
        }

        public override void ApplyOn(Player player, int gameType)
        {
            player.UpdateLocation(new Square(player.GetPlayerLocation() - Value), gameType);
        }
    }

}
