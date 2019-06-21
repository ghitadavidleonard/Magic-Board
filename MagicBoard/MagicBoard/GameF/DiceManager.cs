using System;
using System.Collections.Generic;

namespace MagicBoard
{
    public class DiceManager
    {
        public List<Dice> Dices { get; private set; }
        public DiceManager(int diceCount, int faces = 6)
        {
            Dices = new List<Dice>(diceCount);

            for (int i = 0; i < diceCount; i++)
            {
                Dices.Add(new Dice(faces));
            }
        }

        public bool AllEqual()
        {
            if (Dices.Count < 2)
            {
                return false;
            }
            else
            {
                int i = 0;
                foreach (Dice dice in Dices)
                {
                    if (Dices[0].CurrentValue != Dices[i++].CurrentValue)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public void Roll(Random rnd)
        {
            foreach (Dice dice in Dices)
            {
                dice.Roll(rnd);
            }
        }

        public int GetTotal()
        {
            if (Dices.Count >= 2)
            {
                int total = 0;
                foreach (Dice dice in Dices)
                {
                    int temp = 0;
                    total += Dices[temp++].CurrentValue;
                }
                return total;
            }
            else
            {
                return Dices[0].CurrentValue;
            }
        }

    }

}
