namespace MagicBoard
{
    public abstract class Card : INamable
    {
        /// <summary>
        /// Base class for all cards
        /// </summary>
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Value { get; private set; }

        protected Card(string title, string description, int value)
        {
            Name = title;
            Description = description;
            Value = value;
        }

        ///<summary>
        ///Abstract method we use to define what will happen to the participant
        /// </summary>
        public abstract void ApplyOn(Player player, int gameType);

    }

}
