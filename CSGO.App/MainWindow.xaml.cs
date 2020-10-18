using CSGO.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSGO.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game Game { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Inject_btn_Click(object sender, RoutedEventArgs e)
        {
            Game = new Game();

            if (Game.IsValid())
            {
                status_lbl.Foreground = Brushes.Green;
                status_lbl.Content = "Succeeded";

                inject_btn.IsEnabled = false;
                
            } else
            {
                status_lbl.Foreground = Brushes.Red;
                status_lbl.Content = "failed";
            }
        }
    }
}
