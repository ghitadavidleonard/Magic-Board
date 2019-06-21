namespace MagicBoard
{
    public class ScorableComputerPlayer : ComputerPlayer, IScorable
    {
        public int Score { get; private set; }
        public ScorableComputerPlayer(string name, Square location, int score) : base(name, location)
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
