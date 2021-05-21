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
using WPLClassLibraryTeam13.Data;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Tools;

namespace WpfWebshopTeam13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        SolidColorBrush rood = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD6D6"));
        Admin admin = new Admin();
        public void BtnInloggen_Click(object sender, RoutedEventArgs e)
        {

            QueryAdmin qA = new QueryAdmin();

            //kijk of username bestaat
            SQLCommandResult result = qA.LogIn(TxtGebruikersNaam.Text);
            //Naam.Name = TxtGebruikersNaam.Text;
            //admin.AdminUsername = TxtGebruikersNaam.Text;
            if (result.DataTable.Rows.Count > 0)
            {
                LblFoutUsername.Content = string.Empty;
                var dr = result.DataTable.Rows[0];
                if (TxtWachtwoord.Password != dr[Admin.Database.Columns.AdminPaswoord].ToString())
                {
                    LblFoutWachtwoord.Content = "Wachtwoord is niet juist!";
                    TxtWachtwoord.Background = rood;
                }
                else
                {   //De gebruiker zijn identiteit wordt opgeslagen in een klasse, die wordt doorgegeven via parameters aan de verschillende schermen
                    admin.AdminID = int.Parse(dr[Admin.Database.Columns.AdminId].ToString());
                    admin.AdminNaam = dr[Admin.Database.Columns.AdminNaam].ToString();
                    admin.AdminUsername = dr[Admin.Database.Columns.AdminUsername].ToString();
                    admin.FullAcces = dr[Admin.Database.Columns.FullAccess].ToString();
                    admin.Afbeeldingen_wpf = dr[Admin.Database.Columns.Afbeeldingen_wpf].ToString();

                    //fullAcces = dr[Admin.Database.Columns.FullAccess].ToString();

                    //id = int.Parse(dr[Admin.Database.Columns.AdminId].ToString());

                    LblFoutWachtwoord.Content = string.Empty;
                    TxtGebruikersNaam.Clear();
                    TxtWachtwoord.Clear();
                    WpfKeuzeScherm wpf = new WpfKeuzeScherm(admin);
                    wpf.Owner = this;
                    this.Visibility = Visibility.Hidden;
                    wpf.ShowDialog();
                }
            }
            else
            {
                LblFoutUsername.Content = "Foute username ingegeven";
                TxtGebruikersNaam.Background = rood;
            }
        }

        //public class Naam
        //{
        //    private static string name = "";

        //    public static string Name
        //    {
        //        get { return name; }
        //        set { name = value; }
        //    }
        //}

        private void BtnSluiten_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TxtGebruikersNaam_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtGebruikersNaam.Background = Brushes.White;
            LblFoutUsername.Content = "";
        }

        private void TxtWachtwoord_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtWachtwoord.Background = Brushes.White;
            LblFoutWachtwoord.Content = "";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //bij het inlogscherm wordt de opgeslagen admin in de 'session storage' verwijderd.
            admin = new Admin();
            //dit dient enkel om te testen of de admin goed wordt doorgegeven tussen schermen
            //AdminHelper.TestAdminData(admin);
        }


    }
}
