namespace MagicBoard
{
    class ScoreCard : Card
    {
        public ScoreCard(string title, string description, int value)
            : base(title, description, value) { }

        public override void ApplyOn(Player player, int gameType)
        {
            if (player is IScorable obj)
            {
                obj.UpdateScore(Value);
            }
        }
    }

}
