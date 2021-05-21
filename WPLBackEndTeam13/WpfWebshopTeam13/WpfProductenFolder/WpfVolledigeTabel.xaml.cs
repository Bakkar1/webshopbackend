using System;
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
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;

namespace WpfWebshopTeam13.WpfProductenFolder
{
    /// <summary>
    /// Interaction logic for WpfVolledigeTabel.xaml
    /// </summary>
    public partial class WpfVolledigeTabel : Window
    {
        Admin admin = new Admin();
        public WpfVolledigeTabel(Admin a)
        {
            InitializeComponent();
            admin = a;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryProductDetails qpd = new QueryProductDetails();
            
            var result = qpd.SelectAllRecords();
            DgVolledigeTabel.ItemsSource = result.DefaultView;
        }

        private void DgVolledigeTabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DgVolledigeTabel.SelectedIndex > -1)
            {
                var row = (DataRowView)DgVolledigeTabel.SelectedItem;
                var id = row["productnummer"].ToString();
                var categorie = row["categorie"].ToString();
                Headset h;
                Productdetails p = new Productdetails();
                int productnummer = int.Parse(row["productnummer"].ToString());
                if (categorie == "headset")
                {
                    h = new Headset();
                    p = new Productdetails();

                    WpfHeadsetBewerken wpfhb = new WpfHeadsetBewerken(productnummer, admin);
                    wpfhb.ShowDialog();
                }
                else if (categorie == "laptop")
                {

                    WpfLaptopBewerken wpflb = new WpfLaptopBewerken(productnummer, admin);
                    wpflb.ShowDialog();
                }
                else if (categorie == "muis")
                {

                    WpfMuisBewerken wpfmb = new WpfMuisBewerken(productnummer, admin);
                    wpfmb.ShowDialog();
                }
                else if (categorie == "toetsenbord")
                {
                    WpfToetsenbordBewerken wpftb = new WpfToetsenbordBewerken(productnummer, admin);
                    wpftb.ShowDialog();

                }

            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Owner.Visibility = Visibility.Visible;

            //Door wpfinsertdata terug opnieuw op te roepen, is hij meteen geupdate
            WpfInsertData wpf = new WpfInsertData(admin);
            wpf.ShowDialog(); ;

        }
    }
}

