using System.Collections.Generic;

namespace MagicBoard
{
    public class PathBoard : LinearBoard
    {
        public PathBoard(int squareCount) : base(squareCount) { }

        public bool APlayerHasFinished(List<Player> pl, int nrOfSquares)
        {
            foreach (var player in pl)
            {
                if (player.GetPlayerLocation() >= nrOfSquares)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
