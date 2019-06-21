using System.Collections.Generic;

namespace MagicBoard
{
    public class TurnMaker
    {
        public int TurnCount { get; private set; }

        public TurnMaker(int turnCount)
        {
            TurnCount = turnCount;
        }

        public bool ATurnHasEnded(List<Player> players, ref int index)
        {
            if (players.Count == index)
            {
                TurnCount -= 1;
                index = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AllTurnsAreMade()
        {
            if (TurnCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
