using System;
using BC = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Data;
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
using WPLClassLibraryTeam13.Data;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;

namespace WpfWebshopTeam13
{
    /// <summary>
    /// Interaction logic for WpfUserdata_1.xaml
    /// </summary>
    public partial class WpfUserdata : Window
    {
        public WpfUserdata()
        {
            InitializeComponent();
        }
        private UserData GetUserdata()
        {
            UserData userdata = new UserData();
            userdata.Naam = TxtNaam.Text;
            userdata.VoorNaam = TxtVoornaam.Text;
            userdata.EmailAdres = TxtEmail.Text;
            userdata.Telefoon = TxtTelefoon.Text;
            userdata.StraatNaam = TxtStraat.Text;
            userdata.HuisNummer = TxtHuisnummer.Text;
            userdata.PostCode = TxtPostcode.Text;
            //userdata.Plaats = TxtPlaats.Text;
            //userdata.Land = TxtLand.Text;
            userdata.Wachtwoord = TxtWachtwoord.Text;
            userdata.Aangemaakt = DateTime.Now;
            return userdata;
        }

        private void BtnCommand_Click(object sender, RoutedEventArgs e)
        {
            Rectangle btn = (Rectangle)sender;
            if (btn.Name == "start")
            {

            }
            UserData user =  GetUserdata();
            user.Insert();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int klantnummer = 0;
            if (int.TryParse(LblKlantnummer.Content.ToString(), out klantnummer))
            {
                UserData user = GetUserdata();
                user.KlantNummer = klantnummer;
                user.Update();

                QueryUserData ql = new QueryUserData();
                DgdUserdata_1.ItemsSource = ql.GetData().DataTable.DefaultView;
            }
        }

        private void DgdUserdata_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgdUserdata_1.SelectedIndex > -1)
            {
                var row = (DataRowView)DgdUserdata_1.SelectedItem;
                var klantnummer = row["klantnummer"].ToString();
                MessageBox.Show(klantnummer);
                UpdateForm(klantnummer);
            }
        }
        private void UpdateForm(string klantnummer)
        {
            QueryUserData ql = new QueryUserData();
            foreach (DataRow row in ql.GetData().DataTable.Rows)
            {
                if (row["Klantnummer"].ToString() == klantnummer)
                {
                    LblKlantnummer.Content = klantnummer;
                    TxtNaam.Text = row["Naam"].ToString();
                    TxtVoornaam.Text = row["Voornaam"].ToString();
                    TxtEmail.Text = row["Emailadres"].ToString();
                    TxtTelefoon.Text = row["Telefoon"].ToString();
                    TxtStraat.Text = row["Straatnaam"].ToString();
                    TxtHuisnummer.Text = row["Huisnummer"].ToString();
                    TxtPostcode.Text = row["Postcode"].ToString();
                    TxtWachtwoord.Text = row["Wachtwoord"].ToString();

                    //DateTime dtm = DateTime.Now;
                    //if (DateTime.TryParse(row["Aangemaakt"].ToString(), out dtm))
                    //{
                    //    DtmAangemaakt.SelectedDate = dtm;
                    //}
                    BtnUpdate.Visibility = Visibility.Visible;
                    break;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryUserData ql = new QueryUserData();
            DgdUserdata_1.ItemsSource = ql.GetData().DataTable.DefaultView;
        }
    }
}
