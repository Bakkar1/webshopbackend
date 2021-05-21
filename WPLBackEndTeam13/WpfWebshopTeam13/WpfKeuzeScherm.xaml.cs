using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using WpfWebshopTeam13;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Tools;
using static WpfWebshopTeam13.MainWindow;

namespace WpfWebshopTeam13
{
    /// <summary>
    /// Interaction logic for WpfKeuzeScherm.xaml
    /// </summary>


    public partial class WpfKeuzeScherm : Window
    {
        private string fullAccessEnabled;

        Admin admin = new Admin();

        public WpfKeuzeScherm(Admin a)
        {
            fullAccessEnabled = admin.FullAcces;
            InitializeComponent();
            admin = a;
            // DispatchTimer setup.
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            // Toont de ingelogde gebruiker.
            LblIngelogdeUser.Content = $"Ingelogde gebruiker:  {admin.AdminNaam}";
        }
        public WpfKeuzeScherm()
        {
            fullAccessEnabled = admin.FullAcces;
            InitializeComponent();

            // DispatchTimer setup.
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            // Toont de ingelogde gebruiker.
            LblIngelogdeUser.Content = $"Ingelogde gebruiker:  {admin.AdminNaam}";
        }

        // Houd de tijd/datum up-to-date.
        void timer_Tick(object sender, EventArgs e)
        {
            LblTijd.Content = $"Tijd: " + DateTime.Now.ToLongTimeString() + "    |   " + "Datum: " + DateTime.Now.ToShortDateString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ////dient enkel om te testen of de admin goed wordt doorgegeven tussen schermen
            //AdminHelper.TestAdminData(admin);

            if (fullAccessEnabled == "N")
            {
                BtnKlantBestand.IsEnabled = false;
                BtnRechten.IsEnabled = false;
            }
        }

        private void BtnSluiten_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnAfmelden_Click(object sender, RoutedEventArgs e)
        {
            //Mainwindow heeft geen parameter
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            //Owner.Visibility = Visibility.Visible;
        }

        private void LstView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstView.SelectedIndex == 0)
            {
                // KLANTENBESTAND

                LstView.SelectedIndex = -1;
            }

            else if (LstView.SelectedIndex == 1)
            {
                // ORDERS

                LstView.SelectedIndex = -1;
            }

            else if (LstView.SelectedIndex == 2)
            {
                // PRODUCTEN
                WpfInsertData wpf = new WpfInsertData(admin);
                // wpf.Owner = this;
                //this.Visibility = Visibility.Hidden;
                wpf.Show();
                this.Close();
                //LstView.SelectedIndex = -1;
            }

            else if (LstView.SelectedIndex == 3)
            {
                // TOEGANGSRECHTEN
                WpfToegangsrechten wpf = new WpfToegangsrechten(admin);
                //wpf.Owner = this;
                //this.Visibility = Visibility.Hidden;
                wpf.Show();
                //LstView.SelectedIndex = -1;
                this.Close();
            }

            else if (LstView.SelectedIndex == 4)
            {
                // HANDLEIDING

                LstView.SelectedIndex = -1;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Owner.Visibility = Visibility.Visible;
        }

        private void BtnMinimaliseren_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
