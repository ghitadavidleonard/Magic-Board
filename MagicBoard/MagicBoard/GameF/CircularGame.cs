using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagicBoard.Game
{
    public class CircularGame : BaseGame
    {
        public CircularBoard CBoard { get; private set; }
        public TurnMaker Tm { get; private set; }

        public CircularGame(TurnMaker tm, CircularBoard board, Random rnd, int numberOfCards, List<Player> players, DiceManager dm) : base(rnd, players, dm)
        {
            CBoard = board;
            Tm = tm;

            for (int i = 0; i < numberOfCards / 3; i++)
            {
                PunishementCard pCard = new PunishementCard("Cursed Square", "Cursed by a powerful enchantress, you started to go back.", Rnd.Next(1, 10));
                Cards.Add(pCard);
                BlessCard bCard = new BlessCard("Blessed Square", "The gods have seen your worthy spirit and guided your steps!", Rnd.Next(1, 10));
                Cards.Add(bCard);
                ScoreCard sCard = new ScoreCard("Score Affected", "You steeped on a strange thing that affected your score. I am wondering how...", Rnd.Next(-2000, 2001));
                Cards.Add(sCard);
            }

            CBoard.InsertCards(numberOfCards, Cards);

        }

        public override bool IsFinished()
        {
            if (Tm.AllTurnsAreMade())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Start()
        {
            int parameter = 0;
            while (!IsFinished())
            {
                CircularGameType.main.StateOfGame = $"{Players[0].Name} must throw the dices.";
                Players[0].Input();
                Dm.Roll(Rnd);
                int loc = Players[0].GetPlayerLocation();
                Players[0].UpdateLocation(new Square(Players[0].GetPlayerLocation() + Dm.GetTotal()), 2);
                CBoard.MakeItCircular(Players[0], CBoard.SquareCount);
                if (Players[0] is IScorable scPlayer)
                {
                    if (loc > Players[0].GetPlayerLocation())
                    {
                        scPlayer.UpdateScore(2000);
                        CircularGameType.main.StateOfGame = $"{Players[0].Name} went by start square and got 2000 bonus points. His points are {scPlayer.GetTotalScore()}";
                        #region UpdateScoreForTheUi
                        if (Players[0].Name.Equals("Player 1"))
                        {
                            CircularGameType.main.Player1 = $"Player1: {((ScorableHumanPlayer)Players[0]).GetTotalScore()} points";
                        }
                        else if (Players[0].Name.Equals("Player 2"))
                        {
                            CircularGameType.main.Player2 = $"Player2: {((ScorableHumanPlayer)Players[0]).GetTotalScore()} points";
                        }
                        else if (Players[0].Name.Equals("Computer"))
                        {
                            CircularGameType.main.Player3 = $"Computer: {((ScorableComputerPlayer)Players[0]).GetTotalScore()} points";
                        }
                        #endregion
                        Thread.Sleep(2000);
                    }
                    scPlayer.UpdateScore(Dm.GetTotal() * 10);
                    #region UpdateScoreForTheUi
                    if (Players[0].Name.Equals("Player 1"))
                    {
                        CircularGameType.main.Player1 = $"Player1: {((ScorableHumanPlayer)Players[0]).GetTotalScore()} points";
                    }
                    else if (Players[0].Name.Equals("Player 2"))
                    {
                        CircularGameType.main.Player2 = $"Player2: {((ScorableHumanPlayer)Players[0]).GetTotalScore()} points";
                    }
                    else if(Players[0].Name.Equals("Computer")){
                        CircularGameType.main.Player3 = $"Computer: {((ScorableComputerPlayer)Players[0]).GetTotalScore()} points";
                    }
                    #endregion
                    CircularGameType.main.StateOfGame = $"{Players[0].Name} has throwed {Dm.GetTotal()}. His score is {scPlayer.GetTotalScore()}\n";
                    Thread.Sleep(2000);
                    CBoard.ApplySquareEffect(Players[0]);

                    if (Dm.AllEqual())
                    {
                        CircularGameType.main.StateOfGame = $"\n{Players[0].Name} has thrown a double, he get the chance to throw again...\n";
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        var temp = Players[0];
                        for (int i = 0; i < Players.Count - 1; i++)
                        {
                            Players[i] = Players[i + 1];
                        }
                        Players[Players.Count - 1] = temp;

                        parameter++;
                        if (Tm.ATurnHasEnded(Players, ref parameter))
                        {
                            CircularGameType.main.StateOfGame = $"\nA turn has ended.\n";
                            CircularGameType.main.TurnsLeft = $"Turns Left: {Tm.TurnCount}";
                            Thread.Sleep(1000);
                        }

                    }

                    if (Tm.AllTurnsAreMade())
                    {
                        int max = scPlayer.GetTotalScore();
                        string name;

                        if (scPlayer is ScorableHumanPlayer)
                            name = ((ScorableHumanPlayer)scPlayer).Name;
                        else
                            name = ((ScorableComputerPlayer)scPlayer).Name;

                        foreach (IScorable sc in Players)
                        {
                            if (sc.GetTotalScore() > max)
                            {
                                max = sc.GetTotalScore();
                                if(sc is ScorableHumanPlayer)
                                    name = ((ScorableHumanPlayer)sc).Name;
                                else
                                    name = ((ScorableComputerPlayer)sc).Name;
                            }
                            else
                            {
                            }
                        }
                        CircularGameType.main.StateOfGame = $"\nThe winner is {name} with a total score of {max} points.";
                        Thread.Sleep(2000);
                    }

                }
            }
        }
    }
}
