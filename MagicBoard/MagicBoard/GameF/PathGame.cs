using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MagicBoard
{
    public class PathGame : BaseGame
    {
        public PathBoard PBoard { get; private set; }

        public PathGame(PathBoard board, Random rnd, int numberOfCards, List<Player> players, DiceManager dm) : base(rnd, players, dm)
        {
            PBoard = board;

            for (int i = 0; i < numberOfCards / 2; i++)
            {
                PunishementCard pCard = new PunishementCard("Cursed Square", "Cursed by a powerful enchantress, you started to go back.", Rnd.Next(1, 10));
                Cards.Add(pCard);
                BlessCard bCard = new BlessCard("Blessed Square", "The gods have seen your worthy spirit and guided your steps!", Rnd.Next(1, 10));
                Cards.Add(bCard);
            }

            PBoard.InsertCards(numberOfCards, Cards);
        }

        public override bool IsFinished()
        {
            if (PBoard.APlayerHasFinished(Players, PBoard.SquareCount))
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
            while (!IsFinished())
            {
                PathGameType.main.StateOfGame = $"{Players[0].Name} must throw the dices";
                Players[0].Input();
                Dm.Roll(Rnd);
                if ((Players[0].GetPlayerLocation() + Dm.GetTotal()) >= PBoard.SquareCount - 1)
                {
                    Players[0].UpdateLocation(new Square(PBoard.SquareCount), 1);
                    PathGameType.main.StateOfGame = $"{Players[0].Name} has throwed {Dm.GetTotal()} and he got on the final square";
                    PathGameType.main.StateOfGame = $"{Players[0].Name} is the winner. The game has ended.";
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Players[0].UpdateLocation(new Square(Players[0].GetPlayerLocation() + Dm.GetTotal()), 1);
                    PathGameType.main.StateOfGame = $"{Players[0].Name} has throwed {Dm.GetTotal()} and he got on the {Players[0].GetPlayerLocation()} square";
                    Thread.Sleep(2000);
                }

                PBoard.ApplySquareEffect(Players[0]);

                if (Dm.AllEqual())
                {
                    PathGameType.main.StateOfGame = $"{Players[0].Name} has thrown a double, he get the chance to throw again...";
                    Thread.Sleep(2000);
                }
                else
                {
                    var temp = Players[0];
                    for (int i = 0; i < Players.Count - 1; i++)
                    {
                        Players[i] = Players[i + 1];
                    }
                    Players[Players.Count - 1] = temp;
                }

            }
        }
    }

}
