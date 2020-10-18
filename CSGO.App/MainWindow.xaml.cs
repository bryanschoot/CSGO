using CSGO.Data;
using System;
using System.Windows;
using System.Windows.Media;

namespace CSGO.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game Game { get; set; }
        private Match Match { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Inject_btn_Click(object sender, RoutedEventArgs e)
        {
            Game = new Game();
            Game.Start();

            if (Game.IsValid())
            {
                status_lbl.Foreground = Brushes.Green;
                status_lbl.Content = "Succeeded";

                inject_btn.IsEnabled = false;
                Start();
            }
            else
            {
                status_lbl.Foreground = Brushes.Red;
                status_lbl.Content = "failed";
            }
        }

        private void Start()
        {
            Match = new Match(Game);
            Match.Start();
        }
    }
}
