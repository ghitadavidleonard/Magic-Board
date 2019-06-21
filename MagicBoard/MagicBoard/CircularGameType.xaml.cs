using MagicBoard.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MagicBoard
{
    /// <summary>
    /// Interaction logic for CircularGameType.xaml
    /// </summary>
    public partial class CircularGameType : Window
    {
        private List<Point> _coordonatesPoints = new List<Point>();

        private int _col;
        private int _row;

        private List<Player> _players = new List<Player>();

        internal string StateOfGame
        {
            get { return labelText.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { labelText.Content = value; })); }
        }

        internal string Player1
        {
            get { return player1Score.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { player1Score.Content = value; })); }
        }

        internal string Player2
        {
            get { return player2Score.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { player2Score.Content = value; })); }
        }

        internal string Player3
        {
            get { return player3Score.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { player3Score.Content = value; })); }
        }

        internal string TurnsLeft
        {
            get { return labelText.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { turnsLeft.Content = value; })); }
        }

        internal static CircularGameType main;
        Thread gs;

        public CircularGame Game { get; private set; }
        public Dictionary<string, TextBlock> PosAndName = new Dictionary<string, TextBlock>();
        public CircularGameType() 
        {
            InitializeComponent();
            main = this;
            CenterWindowOnScreen();
            _col = 10; _row = 10;
            //Create the players based on the user choise

            InitializeThePlayers();

            Game = new CircularGame(new TurnMaker(3), new CircularBoard(36), new Random(), 9, _players, new DiceManager(2));
            turnsLeft.Content = "Turns Left: 3";
            MakeTheGrid(_col, _row);

            gs = new Thread(() =>
            {
                Game.Start();
            });
            gs.Start();
        }

        private void InitializeThePlayers()
        {
            if (MainWindow.NrOfPlayers == 1)
            {
                Player p1 = new ScorableHumanPlayer("Player 1", new Square(0), 1000);
                Player p2 = new ScorableComputerPlayer("Computer", new Square(0), 1000);
                _players.Add(p1);
                player1Score.Content = $"Player1: {((ScorableHumanPlayer)p1).GetTotalScore()} points";
                _players.Add(p2);
                player2Score.Content = $"Computer: {((ScorableComputerPlayer)p2).GetTotalScore()} points";
            }
            else if (MainWindow.NrOfPlayers == 2)
            {
                Player p1 = new ScorableHumanPlayer("Player 1", new Square(0), 1000);
                Player p2 = new ScorableHumanPlayer("Player 2", new Square(0), 1000);
                _players.Add(p1);
                player1Score.Content = $"Player1: {((ScorableHumanPlayer)p1).GetTotalScore()} points";
                _players.Add(p2);
                player2Score.Content = $"Player2: {((ScorableHumanPlayer)p2).GetTotalScore()} points";
            }
            else if (MainWindow.NrOfPlayers == 3)
            {
                Player p1 = new ScorableHumanPlayer("Player 1", new Square(0), 1000);
                Player p2 = new ScorableHumanPlayer("Player 2", new Square(0), 1000);
                Player p3 = new ScorableComputerPlayer("Computer", new Square(0), 1000);
                _players.Add(p1);
                player1Score.Content = $"Player1: {((ScorableHumanPlayer)p1).GetTotalScore()} points";
                _players.Add(p2);
                player2Score.Content = $"Player2: {((ScorableHumanPlayer)p2).GetTotalScore()} points";
                _players.Add(p3);
                player3Score.Content = $"Computer: {((ScorableComputerPlayer)p3).GetTotalScore()} points";
            }
        }

        private void MakeTheGrid(int col, int row)
        {
            int j;
            int i;
            do
            {
                for(j = 0, i = 0; j < col; j++)
                {
                    _coordonatesPoints.Add(new Point(i, j));
                }

                for(j = col - 1, i = 1; i < row; i++)
                {
                    _coordonatesPoints.Add(new Point(i, j));
                }

                for(j = col - 2, i = row - 1; j >= 0; j--)
                {
                    _coordonatesPoints.Add(new Point(i, j));
                }

                for(j = 0, i = row-1; i > 0; i--)
                {
                    _coordonatesPoints.Add(new Point(i, j));
                }

            } while (_coordonatesPoints.Count == Game.CBoard.SquareCount);

            int k = 0;

            foreach (var item in _coordonatesPoints)
            {
                Rectangle rect = new Rectangle();
                TextBlock txt = new TextBlock();
                txt.Text = k.ToString();
                txt.Foreground = new SolidColorBrush(Colors.White);
                rect.RadiusX = 5;
                rect.RadiusY = 5;

                rect.Stroke = new SolidColorBrush(Colors.Black);
                if (k == 0)
                    rect.Fill = new SolidColorBrush(Colors.Green);
                else
                    rect.Fill = new SolidColorBrush(Colors.Blue);

                rect.Width = circGameCanvas.Width / col;
                rect.Height = circGameCanvas.Height / row;
                circGameCanvas.Children.Add(rect);
                Canvas.SetTop(rect, ((int)item.X) * (circGameCanvas.Height / row));
                Canvas.SetLeft(rect, ((int)item.Y) * (circGameCanvas.Width / col));

                circGameCanvas.Children.Add(txt);
                Canvas.SetTop(txt, ((int)item.X) * (circGameCanvas.Height / row) + 3);
                Canvas.SetLeft(txt, ((int)item.Y) * (circGameCanvas.Width / col) + (circGameCanvas.Width / col) - 17);
                k++;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PressedOrNot = false;
            PosAndName.Clear();
            ChoseTheGame ctg = new ChoseTheGame();
            ctg.Show();
            gs.Abort();
            Application.Current.MainWindow = null;
            this.Close();
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        internal void LocationUpdate(int value, string name)
        {
            Dispatcher.Invoke(() =>
            {
                TextBlock txt1 = new TextBlock();
                txt1.Text = name;
                txt1.FontSize = 12;
                txt1.Foreground = new SolidColorBrush(Colors.White);
                
                try
                {
                    PosAndName.Add(name, txt1);
                }
                catch (ArgumentException)
                {
                    PosAndName[name] = txt1;
                }
                Canvas.SetTop(txt1, ((int)_coordonatesPoints[value >= _coordonatesPoints.Count - 1 ? _coordonatesPoints.Count - 1 : value].X) * (circGameCanvas.Height / _row) + 15);
                Canvas.SetLeft(txt1, ((int)_coordonatesPoints[value >= _coordonatesPoints.Count - 1 ? _coordonatesPoints.Count - 1 : value].Y) * (circGameCanvas.Width / _col) + 17);
                circGameCanvas.Children.Add(txt1);
            });

        }

        public void circGameRollBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PressedOrNot = true;
        }

        internal void DeleteDictionaryEntry(string name)
        {
            if (PosAndName.ContainsKey(name))
            {
                Dispatcher.Invoke(() =>
                {
                    circGameCanvas.Children.Remove(PosAndName[name]);
                });
                PosAndName.Remove(name);
            }
        }
    }
}
