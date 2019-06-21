using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ChoseTheGame.xaml
    /// </summary>
    public partial class ChoseTheGame : Window
    {
        public ChoseTheGame()
        {
            InitializeComponent();
            CenterWindowOnScreen();
        }

        private void CircularChoose_Click(object sender, RoutedEventArgs e)
        {
            CircularGameType cgt = new CircularGameType();
            cgt.Show();
            Application.Current.MainWindow = null;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PathGameType pgt = new PathGameType();
            pgt.Show();
            Application.Current.MainWindow = null;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
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

    }
}
