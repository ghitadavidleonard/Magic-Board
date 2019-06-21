using System;
using System.Collections.Generic;

namespace MagicBoard
{
    public abstract class Board
    {
        /// <summary>
        /// Base class for boards
        /// </summary>
        public int SquareCount { get; private set; }
        public List<Square> squares = new List<Square>();

        public Board(int squareCount)
        {
            SquareCount = squareCount;
        }

        /// <summary>
        /// Abstract method for building the board
        /// </summary>
        public abstract void BuildBoard();

        public void InsertCards(int numberOfCards, List<Card> card)
        {
            int index;
            Random rnd = new Random();

            for (int i = 0; i < numberOfCards; i++)
            {
                do
                {
                    index = rnd.Next(1, SquareCount);
                } while (squares[index].Card != null);

                squares[index] = new Square(index, card[i]);
            }
        }

        public abstract void ApplySquareEffect(Player player);

    }

}
