namespace MagicBoard
{
    public class ScorableHumanPlayer : HumanPlayer, IScorable
    {
        public int Score { get; private set; }
        public ScorableHumanPlayer(string name, Square location, int score)
            : base(name, location)
        {
            Score = score;
        }


        public int GetTotalScore()
        {
            return Score;
        }

        public void UpdateScore(int score)
        {
            Score += score;
        }
    }

}
