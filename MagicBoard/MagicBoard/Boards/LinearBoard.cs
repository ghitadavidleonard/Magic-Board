using System;
using System.Threading;

namespace MagicBoard
{
    public class LinearBoard : Board
    {
        public LinearBoard(int squareCount) : base(squareCount)
        {
            BuildBoard();
        }

        public override void ApplySquareEffect(Player player)
        {
            while (squares[player.GetPlayerLocation()].Card != null)
            {
                if (squares[player.GetPlayerLocation()].Card.GetType() == typeof(PunishementCard))
                {

                    PathGameType.main.StateOfGame = $"The player landed on a cursed square!\n{squares[player.GetPlayerLocation()].Card.Name} \n" +
                        $"{squares[player.GetPlayerLocation()].Card.Description}\n And so he went back " +
                        $"{squares[player.GetPlayerLocation()].Card.Value}";
                    Thread.Sleep(5000);
                    if ((player.GetPlayerLocation() - squares[player.GetPlayerLocation()].Card.Value) < 0)
                    {

                        player.UpdateLocation(new Square(0), 1);
                        PathGameType.main.StateOfGame  = $"{player.Name} is now on the first square";
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        squares[player.GetPlayerLocation()].Card.ApplyOn(player, 1);
                    }
                }
                else
                {
                    PathGameType.main.StateOfGame = $"The player landed on a blessed square!\n{squares[player.GetPlayerLocation()].Card.Name} \n" +
                        $"{squares[player.GetPlayerLocation()].Card.Description}\n And so he went forward " +
                        $"{squares[player.GetPlayerLocation()].Card.Value}";
                    Thread.Sleep(5000);
                    int lastposition = player.GetPlayerLocation();
                    squares[player.GetPlayerLocation()].Card.ApplyOn(player, 1);

                    if (player.GetPlayerLocation() >= SquareCount)
                    {
                        PathGameType.main.StateOfGame = $"{player.Name} has throwed {player.GetPlayerLocation() - lastposition} and he got on the final square";
                        PathGameType.main.StateOfGame += $"\n{player.Name} is the winner. The game has ended.";
                        Thread.Sleep(5000);
                        break;
                    }
                }
            }
        }

        public override void BuildBoard()
        {
            for (int i = 0; i < SquareCount; i++)
            {
                Square square = new Square(i);
                squares.Add(square);
            }
        }
    }

}
