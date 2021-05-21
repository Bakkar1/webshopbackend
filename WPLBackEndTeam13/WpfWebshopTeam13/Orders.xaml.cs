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
using WPLClassLibraryTeam13.Query;

namespace WpfWebshopTeam13
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
        }
        private string[] mogelijkeStatus = { "Pending", "Delevired"};

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryOrders qlOrders = new QueryOrders();
            DtGridOrders.ItemsSource = qlOrders.getJoinData().DefaultView;

            //comobbox op vullen
            foreach(string status in mogelijkeStatus)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = status;
                CmbStatus.Items.Add(item);
            }
        }

        private void CmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CmbStatus.SelectedIndex > -1)
            {
                QueryOrders qlOrders = new QueryOrders();
                string status = ((ComboBoxItem)CmbStatus.SelectedItem).Content.ToString();
                int id;
                if (int.TryParse(TxtKlantNummer.Text, out id))
                {
                    DtGridOrders.ItemsSource = qlOrders.getJoinDataStatus(status, id).DefaultView;
                }
                else
                {
                    DtGridOrders.ItemsSource = qlOrders.getJoinDataStatus(status).DefaultView;
                }
                
            }
        }
        private void TxtKlantNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            int id;
            var bc = new BrushConverter();
            if (int.TryParse(TxtKlantNummer.Text, out id))
            {
                TxtKlantNummer.Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
                QueryOrders qlOrders = new QueryOrders();
                if (CmbStatus.SelectedIndex > -1)
                {
                    string status = ((ComboBoxItem)CmbStatus.SelectedItem).Content.ToString();
                    DtGridOrders.ItemsSource = qlOrders.getJoinDataStatus(status,id).DefaultView;
                }
                else
                {
                    DtGridOrders.ItemsSource = qlOrders.getJoinData(id).DefaultView;
                }
               
                
            }
            else
            {
                TxtKlantNummer.Background = (Brush)bc.ConvertFrom("#FFFF0000");
            }
        }

        private void DtPickerOrders_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime date;
            if(DateTime.TryParse(DtPickerOrders.Text,out date))
            {
                QueryOrders qlOrders = new QueryOrders();
                DtGridOrders.ItemsSource = qlOrders.GetJoinDataDate(date).DefaultView;
            }
        }
    }
}
