using System;

namespace MagicBoard
{
    public class Dice
    {
        public int Faces { get; private set; }
        public int CurrentValue { get; private set; }

        public Dice(int faces)
        {
            Faces = faces;
            CurrentValue = 0;
        }

        public int Roll(Random rnd)
        {

            CurrentValue = rnd.Next(1, Faces + 1);
            return CurrentValue;
        }
    }


}
