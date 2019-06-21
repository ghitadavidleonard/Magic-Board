using System;
using System.Windows.Controls;
using static MagicBoard.MainWindow;

namespace MagicBoard
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, Square location)
            : base(name, location) { }

        public override void Input()
        {
            while (!PressedOrNot)
            {

            }
            PressedOrNot = false;
        }

    }
}
