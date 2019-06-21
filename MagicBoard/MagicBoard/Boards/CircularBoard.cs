using System;
using System.Linq;
using System.Threading;

namespace MagicBoard
{
    public class CircularBoard : Board
    {
        public int StartSquare { get; set; }

        public CircularBoard(int squareCount) : base(squareCount)
        {
            BuildBoard();
            //LinkBoardSquares();
        }

        public override void BuildBoard()
        {
            for (int i = 0; i < SquareCount; i++)
            {
                Square square = new Square(i);
                squares.Add(square);
            }
        }

        public void MakeItCircular(Player player, int nrOfSquares)
        {
            if (player.GetPlayerLocation() < 0)
            {
                player.UpdateLocation(new Square(player.GetPlayerLocation() + nrOfSquares), 2);
            }
            else if (player.GetPlayerLocation() >= nrOfSquares)
            {
                player.UpdateLocation(new Square(player.GetPlayerLocation() - nrOfSquares), 2);
            }
        }

        public override void ApplySquareEffect(Player player)
        {
            int loc = 0;

            while (squares.ElementAt(player.GetPlayerLocation()).Card != null)
            {
                if (squares.ElementAt(player.GetPlayerLocation()).Card.GetType() == typeof(PunishementCard))
                {
                    CircularGameType.main.StateOfGame = $"The player landed on a cursed square!\n{squares.ElementAt(player.GetPlayerLocation()).Card.Name} \n" +
                        $"{squares.ElementAt(player.GetPlayerLocation()).Card.Description}\n And so he went back " +
                        $"{squares.ElementAt(player.GetPlayerLocation()).Card.Value}";
                    Thread.Sleep(4000);

                    squares.ElementAt(player.GetPlayerLocation()).Card.ApplyOn(player, 2);

                    MakeItCircular(player, SquareCount);

                    CircularGameType.main.StateOfGame = $"{player.Name} is now on the {player.GetPlayerLocation()} square";
                    Thread.Sleep(2000);
                }
                else if (squares.ElementAt(player.GetPlayerLocation()).Card.GetType() == typeof(BlessCard))
                {
                    CircularGameType.main.StateOfGame = $"The player landed on a blessed square!\n{squares.ElementAt(player.GetPlayerLocation()).Card.Name} \n" +
                        $"{squares.ElementAt(player.GetPlayerLocation()).Card.Description}\n And so he went forward " +
                        $"{squares.ElementAt(player.GetPlayerLocation()).Card.Value}";
                    Thread.Sleep(4000);
                    squares.ElementAt(player.GetPlayerLocation()).Card.ApplyOn(player, 2);
                    loc = player.GetPlayerLocation();
                    MakeItCircular(player, SquareCount);
                    if (loc > player.GetPlayerLocation())
                    {
                        if (player is IScorable scPlayer)
                        {
                            scPlayer.UpdateScore(2000);
                            #region UpdateScoreForTheUi
                            if (player.Name.Equals("Player 1"))
                            {
                                CircularGameType.main.Player1 = $"Player1: {((ScorableHumanPlayer)player).GetTotalScore()} points";
                            }
                            else if (player.Name.Equals("Player 2"))
                            {
                                CircularGameType.main.Player2 = $"Player2: {((ScorableHumanPlayer)player).GetTotalScore()} points";
                            }
                            else if (player.Name.Equals("Computer"))
                            {
                                CircularGameType.main.Player3 = $"Computer: {((ScorableComputerPlayer)player).GetTotalScore()} points";
                            }
                            #endregion
                            CircularGameType.main.StateOfGame = $"{player.Name} went by start square and got 2000 bonus points. His points are {scPlayer.GetTotalScore()}";
                            Thread.Sleep(2000);
                        }
                    }
                    else
                    {
                        CircularGameType.main.StateOfGame = $"{player.Name} is now on the {player.GetPlayerLocation()} square";
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    CircularGameType.main.StateOfGame = $"The player landed on a strange square!\n{squares.ElementAt(player.GetPlayerLocation()).Card.Name} \n" +
                        $"{squares.ElementAt(player.GetPlayerLocation()).Card.Description}\n";
                    Thread.Sleep(4000);
                    if (player is IScorable scPlayer)
                    {
                        scPlayer.UpdateScore(squares.ElementAt(player.GetPlayerLocation()).Card.Value);
                        #region UpdateScoreForTheUi
                        if (player.Name.Equals("Player 1"))
                        {
                            CircularGameType.main.Player1 = $"Player1: {((ScorableHumanPlayer)player).GetTotalScore()} points";
                        }
                        else if (player.Name.Equals("Player 2"))
                        {
                            CircularGameType.main.Player2 = $"Player2: {((ScorableHumanPlayer)player).GetTotalScore()} points";
                        }
                        else if(player.Name.Equals("Computer")){
                            CircularGameType.main.Player3 = $"Computer: {((ScorableComputerPlayer)player).GetTotalScore()} points";
                        }
                        #endregion
                        CircularGameType.main.StateOfGame = $"Now, his score is {scPlayer.GetTotalScore()}";
                        Thread.Sleep(2000);
                    }
                    break;
                }
            }
        }
    }
}