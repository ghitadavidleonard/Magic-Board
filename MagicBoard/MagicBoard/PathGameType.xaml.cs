using MagicBoard.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace MagicBoard
{
    /// <summary>
    /// Interaction logic for PathGame.xaml
    /// </summary>
    public partial class PathGameType : Window
    {
        private List<Point> coordonatesPoints = new List<Point>();

        private int row;
        private int col;

        internal static PathGameType main;
        internal string StateOfGame
        {
            get { return InfoLabel.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { InfoLabel.Content = value; })); }
        }
        public PathGame Game { get; private set; }

        public Dictionary<string, TextBlock> PosAndName = new Dictionary<string, TextBlock>();
        Thread thread;

        private List<Player> players = new List<Player>();
        
        public PathGameType()
        {
          //Initialize the components
            InitializeComponent();
            main = this;
            CenterWindowOnScreen();
            col = 10; row = 10;
            PathGenerator pg = new PathGenerator(25, col, row);
            coordonatesPoints = pg.GeneratePath();
            MakeTheGrid(col, row);

            //Create the players based on the user choise
            InitializeThePlayers();

            //Intialize the game and put it on a new thread so the UI doesn't freez
            Game = new PathGame(new PathBoard(25), new Random(), 6, players, new DiceManager(1));
            thread = new Thread(() =>
            {
                Game.Start();
            });
            thread.Start();

        }
        public void rollBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PressedOrNot = true;
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.PressedOrNot = false;
            PosAndName.Clear();
            ChoseTheGame ctg = new ChoseTheGame();
            ctg.Show();
            thread.Abort();
            Application.Current.MainWindow = null;
            this.Close();
        }

        private void InitializeThePlayers()
        {
            if (MainWindow.NrOfPlayers == 1)
            {
                Player p1 = new HumanPlayer("Player 1", new Square(0));
                Player p2 = new ComputerPlayer("Computer", new Square(0));
                players.Add(p1);
                players.Add(p2);
            }
            else if (MainWindow.NrOfPlayers == 2)
            {
                Player p1 = new HumanPlayer("Player 1", new Square(0));
                Player p2 = new HumanPlayer("Player 2", new Square(0));
                players.Add(p1);
                players.Add(p2);
            }
            else if (MainWindow.NrOfPlayers == 3)
            {
                Player p1 = new HumanPlayer("Player 1", new Square(0));
                Player p2 = new HumanPlayer("Player 2", new Square(0));
                Player p3 = new ComputerPlayer("Computer", new Square(0));
                players.Add(p1);
                players.Add(p2);
                players.Add(p3);
            }
        }

        //Utility funtions
        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        private void MakeTheGrid(int col, int row)
        {
            int i = 0;

            foreach(var item in coordonatesPoints)
            {
                Rectangle rect = new Rectangle();
                TextBlock txt = new TextBlock();
                txt.Text = i.ToString();
                txt.Foreground = new SolidColorBrush(Colors.White);
                rect.RadiusX = 5;
                rect.RadiusY = 5;
                
                rect.Stroke = new SolidColorBrush(Colors.Black);
                if (i == coordonatesPoints.Count - 1)
                    rect.Fill = new SolidColorBrush(Colors.Red);
                else if (i == 0)
                    rect.Fill = new SolidColorBrush(Colors.Green);
                else
                    rect.Fill = new SolidColorBrush(Colors.Blue);

                rect.Width = theCanvas.Width / col;
                rect.Height = theCanvas.Height / row;
                theCanvas.Children.Add(rect);
                Canvas.SetTop(rect, ((int)item.X) * (theCanvas.Height / row));
                Canvas.SetLeft(rect, ((int)item.Y) * (theCanvas.Width / col));

                theCanvas.Children.Add(txt);
                Canvas.SetTop(txt, ((int)item.X) * (theCanvas.Height / row) + 3);
                Canvas.SetLeft(txt, ((int)item.Y) * (theCanvas.Width / col) + (theCanvas.Width / col) - 17);
                i++;
            }

        }

        internal void LocationUpdate(int value, string name)
        {
            Dispatcher.Invoke(() =>
            {
                TextBlock txt1 = new TextBlock();
                txt1.Text = name;
                txt1.FontSize = 12;
                txt1.Foreground = new SolidColorBrush(Colors.White);
                PosAndName.Add(name, txt1);
                Canvas.SetTop(txt1, ((int) coordonatesPoints[value >= coordonatesPoints.Count - 1 ? coordonatesPoints.Count - 1 : value].X) * (theCanvas.Height / row) + 15);
                Canvas.SetLeft(txt1,((int) coordonatesPoints[value >= coordonatesPoints.Count - 1 ? coordonatesPoints.Count - 1 : value].Y) * (theCanvas.Width / col) + 17);
                theCanvas.Children.Add(txt1);
            });

        }

        internal void DeleteDictionaryEntry(string name)
        {
            if (PosAndName.ContainsKey(name))
            {
                Dispatcher.Invoke(() =>
                {
                    theCanvas.Children.Remove(PosAndName[name]);
                });
                PosAndName.Remove(name);
            }
        }
    }
}
