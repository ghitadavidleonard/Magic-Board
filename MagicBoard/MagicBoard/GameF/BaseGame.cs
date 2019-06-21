using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicBoard
{
    public abstract class BaseGame
    {

        public Random Rnd { get; private set; }
        public List<Card> Cards { get; private set; }
        public List<Player> Players { get; private set; }
        public DiceManager Dm { get; private set; }

        public BaseGame(Random rnd, List<Player> players, DiceManager dm)
        {
            Rnd = rnd;
            Cards = new List<Card>();
            Players = players.ToList();
            Dm = dm;
        }

        public abstract void Start();

        public abstract bool IsFinished();
    }

}
