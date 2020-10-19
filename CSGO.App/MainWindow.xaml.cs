using CSGO.Data;
using CSGO.Features;
using System;
using System.Windows;
using System.Windows.Media;

namespace CSGO.App
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private Game Game { get; set; }
        private Match Match { get; set; }
        private Wallhack Wallhack { get; set; }
        private Recoil Recoil { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Disable();
        }

        private void Inject_btn_Click(object sender, RoutedEventArgs e)
        {
            Game = new Game();
            Game.Start();

            if (Game.IsValid())
            {
                status_lbl.Foreground = Brushes.Green;
                status_lbl.Content = "Injected";

                inject_btn.IsEnabled = false;

                Enable();
                Start();
            }
            else
            {
                status_lbl.Foreground = Brushes.Red;
                status_lbl.Content = "Failed";
            }
        }

        private void Disable()
        {
            wallhack_cb.IsEnabled = false;
            recoil_cb.IsEnabled = false;
        }

        private void Enable()
        {
            wallhack_cb.IsEnabled = true;
            recoil_cb.IsEnabled = true;
        }

        private void Start()
        {
            Match = new Match(Game);
            Match.Start();
        }

        private void Wallhack_cb_Checked(object sender, RoutedEventArgs e)
        {
            Wallhack = new Wallhack(Match);
            Wallhack.Start();
        }

        private void Wallhack_cb_UnChecked(object sender, RoutedEventArgs e)
        {
            Wallhack.Dispose();
            Wallhack = default;
        }

        private void Recoil_cb_Checked(object sender, RoutedEventArgs e)
        {
            Recoil = new Recoil(Match);
            Recoil.Start();
        }

        private void Recoil_cb_UnChecked(object sender, RoutedEventArgs e)
        {
            Recoil.Dispose();
            Recoil = default;
        }

        public void Dispose()
        {
            Game.Dispose();
            Match.Dispose();
            Wallhack.Dispose();
            Recoil.Dispose();

            Game = default;
            Match = default;
            Wallhack = default;
            Recoil = default;
        }

 
    }
}
