namespace MagicBoard
{
    public interface IScorable
    {
        int Score { get; }
        void UpdateScore(int score);
        int GetTotalScore();
    }

}
