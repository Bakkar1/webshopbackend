using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Tools;
using MessageBox = System.Windows.MessageBox;


namespace WpfWebshopTeam13
{
    /// <summary>
    /// Interaction logic for WpfToegangsrechten.xaml
    /// </summary>
    public partial class WpfToegangsrechten : Window
    {

        Admin admin = new Admin();
        public WpfToegangsrechten(Admin a)
        {
            InitializeComponent();
            admin = a;
        }
        public WpfToegangsrechten()
        {
            InitializeComponent();

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            GetData();
            RbtnAanpassen.IsChecked = true;
            Txtpad.Text = admin.Afbeeldingen_wpf;
            Txtpad.ToolTip = Txtpad.Text;
            Txtpad.IsEnabled = false;
            ////dient enkel om te testen of de admin goed wordt doorgegeven tussen schermen
            //AdminHelper.TestAdminData(admin);
        }

        private void GetData()
        {
            QueryAdmin qa = new QueryAdmin();
            var result = qa.GetAdmindata();
            DgdGrid.ItemsSource = result.DataTable.DefaultView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WpfKeuzeScherm wpf = new WpfKeuzeScherm(admin);
            wpf.Show();
            //wpf.Owner = this;
            //Owner.Visibility = Visibility.Visible;


        }

        private void RbtnAanpassen_Checked(object sender, RoutedEventArgs e)
        {
            DgdGrid.SelectedIndex = -1;
            EnableTextboxes();
            ClearTextboxes();
            BtnActie.Content = "Aanpassen";
            Txtpad.Clear();
        }

        private void RbtnToevoegen_Checked(object sender, RoutedEventArgs e)
        {
            EnableTextboxes();
            ClearTextboxes();
            BtnActie.Content = "Toevoegen";
            Txtpad.Clear();
        }

        private void RbtnWissen_Checked(object sender, RoutedEventArgs e)
        {
            DgdGrid.SelectedIndex = -1;
            ClearTextboxes();
            DisableTextboxes();
            BtnActie.Content = "Wissen";
            Txtpad.Clear();
        }

        private void EnableTextboxes()
        {
            TxtNaam.IsEnabled = true;
            TxtUsername.IsEnabled = true;
            TxtWachtwoord.IsEnabled = true;
            CbFullAccess.IsEnabled = true;

        }

        private void DisableTextboxes()
        {
            TxtNaam.IsEnabled = false;
            TxtUsername.IsEnabled = false;
            TxtWachtwoord.IsEnabled = false;
            CbFullAccess.IsEnabled = false;

        }

        private void ClearTextboxes()
        {
            TxtNaam.Clear();
            TxtUsername.Clear();
            TxtWachtwoord.Clear();
            CbFullAccess.SelectedIndex = 0;
            TxtUserId.Clear();
        }

        private void DgdGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgdGrid.SelectedIndex != -1 && RbtnToevoegen.IsChecked == false)
            {
                DataRowView drv = (DataRowView)DgdGrid.SelectedItem;
                TxtNaam.Text = drv[Admin.Database.Columns.AdminNaam].ToString();
                TxtUsername.Text = drv[Admin.Database.Columns.AdminUsername].ToString();
                TxtWachtwoord.Text = drv[Admin.Database.Columns.AdminPaswoord].ToString();
                TxtUserId.Text = drv[Admin.Database.Columns.AdminId].ToString();
                if (drv[Admin.Database.Columns.FullAccess].ToString() == "J")
                {
                    CbFullAccess.SelectedIndex = 0;
                }
                else
                {
                    CbFullAccess.SelectedIndex = 1;
                }
            }
        }

        private void BtnActie_Click(object sender, RoutedEventArgs e)
        {
            if (BtnActie.Content.ToString() == "Aanpassen")
            {
                ClearTextboxes();

                //GetData(); werkt blijkbaar niet hier

                QueryAdmin qa = new QueryAdmin();
                var result = qa.GetAdmindata();
                DgdGrid.ItemsSource = result.DataTable.DefaultView;
            }
            else if (BtnActie.Content.ToString() == "Toevoegen")
            {
                QueryAdmin qa = new QueryAdmin();
                //Validatie om te kijken of de username al bestaat
                if (qa.LogIn(TxtUsername.Text).DataTable.Rows.Count < 1)
                {


                    Admin adm = new Admin();
                    adm.AdminNaam = TxtNaam.Text;
                    adm.AdminUsername = TxtUsername.Text;
                    adm.AdminPaswoord = TxtWachtwoord.Text;
                    adm.FullAcces = CbFullAccess.Text;
                    adm.Insert();
                    ClearTextboxes();
                    GetData();
                }
                else
                    MessageBox.Show("Deze gebruiker bestaat al, kies een andere username.");
            }
            else if (BtnActie.Content.ToString() == "Wissen")
            {
                Admin adm = new Admin();
                adm.Delete(Convert.ToInt32(TxtUserId.Text));
                ClearTextboxes();
                GetData();
            }
        }

        string pad;


        //Men kan nooit een pad voor een andere gebruiker kiezen, dit houdt geen steek. Zou het alleen nog beter moeten kunnen aanduiden op het scherm
        private void BtnZoekPad_Click(object sender, RoutedEventArgs e)
        {
            //using System.Windows.Forms
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult dlgresult = fbd.ShowDialog();
            //if (dlgresult == DialogResult.Ok)                  //Heb geen manier gevonden om dit te valideren, maar is misschien ook niet nodig, crasht niet

            pad = fbd.SelectedPath;
            
 ;
            //string eerstedeel = ImageHelper.GetEersteDeelPad(pad);

            if (Directory.Exists(pad))
            {   
                string eerstedeel = System.IO.Directory.GetParent(pad).FullName + "\\";
                ImageHelper.Path = eerstedeel;
                Txtpad.Text = ImageHelper.Path;


                QueryAdmin qA = new QueryAdmin();
                //Steekt het eerste deel van het pad in de database
                var result = qA.SetAdminAfbeeldingenFolder(admin.AdminID, ImageHelper.Path);

                //Refresht het datagrid
                var result2 = qA.GetAdmindata();
                DgdGrid.ItemsSource = result2.DataTable.DefaultView;

                //Update de ingelogde admin
                admin.Afbeeldingen_wpf = ImageHelper.Path;
                ImageHelper.Path = null;
            }
            else MessageBox.Show("Map bestaat niet.");
        }

        private void Txtpad_GotFocus(object sender, RoutedEventArgs e)
        {
            DgdGrid.SelectedIndex = -1;
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            //WpfKeuzeScherm wpf = new WpfKeuzeScherm(admin);
            //wpf.Show();
            //wpf.Owner = this;
            this.Close();
            //Owner.Visibility = Visibility.Visible;
        }
    }
}
