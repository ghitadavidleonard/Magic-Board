namespace MagicBoard
{
    public class Square
    {
        public int Index { get; private set; }

        public Card Card { get; private set; }

        public Square(int index)
        {
            Index = index;
        }

        public Square(int index, Card card)
        {
            Index = index;
            Card = card;
        }
    }

}
