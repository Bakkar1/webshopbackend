using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Data;
using WPLClassLibraryTeam13.Tools;
using WPLClassLibraryTeam13.Entity;
using System.Windows.Threading;
using System;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Data;
using System.Data.SqlClient;
using WpfWebshopTeam13.WpfProductenFolder;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;

namespace WpfWebshopTeam13
{

    /// <summary>
    /// 
    /// Deze WPF wordt gebruikt om het productenbestand te beheren.
    /// 
    /// </summary>

    public partial class WpfInsertData : Window
    {
        Admin admin = new Admin();
        //Haalt de ingelogde admin uit vorig scherm
        public WpfInsertData(Admin a)
        {
            InitializeComponent();
            admin = a;
        }

        #region LOAD

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            
            //ImageHelper.PadExists(admin.Afbeeldingen_wpf);
            
            /// Dit dient nog gevalideerd te worden
            if(admin.Afbeeldingen_wpf == null)
            {
                MessageBox.Show("Selecteer de map 'images' in the toegangsrechtenmenu.");
            }

            LoadData();
            ////dient enkel om te testen of de admin goed wordt doorgegeven tussen schermen
            //AdminHelper.TestAdminData(admin);
        }

        private void LoadData()
        {
            Productdetails pd = new Productdetails();
            QueryProductDetails qpd = new QueryProductDetails();
            var result = qpd.ViewProductDetails();
            //var result = qpd.GetImages();
            CmbCategorie.SelectedItem = CmbAlle;
            DgData.ItemsSource = result.DefaultView;
        }

        //Bij veranderen van de combobox laadt de datagrid de verschillende data
        private void CmbCategorie_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Load();
        }
        private void Load()
        {
            if (CmbCategorie.SelectedItem == CmbAlle)
                LoadData();
            if (CmbCategorie.SelectedItem == CmbHeadsets)
                LoadDataHeadsets();
            if (CmbCategorie.SelectedItem == CmbLaptops)
                LoadDataLaptops();
            if (CmbCategorie.SelectedItem == CmbMuizen)
                LoadDataMuis();
            if (CmbCategorie.SelectedItem == CmbToetsenborden)
                LoadDataToetsenbord();
            if (CmbCategorie.SelectedItem == CmbVolledigeTabel)
            {
                WpfVolledigeTabel wpfvt = new WpfVolledigeTabel(admin);
                wpfvt.Owner = this;
                this.Visibility = Visibility.Hidden;
                Nullable<bool> dialogResult = wpfvt.ShowDialog();
                if (dialogResult == false)
                {
                    CmbCategorie.SelectedIndex = 0;
                }
            }
        }
        private void LoadDataHeadsets()
        {
            Headset h = new Headset();
            QueryHeadset qh = new QueryHeadset();
            var result = qh.ViewHeadsetDetails();

            DgData.ItemsSource = result.DefaultView;
        }
        private void LoadDataLaptops()
        {
            Laptop l = new Laptop();
            QueryLaptop ql = new QueryLaptop();
            var result = ql.ViewLaptopDetails();
            DgData.ItemsSource = result.DefaultView;
        }
        private void LoadDataMuis()
        {
            QueryMuis qm = new QueryMuis();
            var result = qm.ViewMuisDetails();
            DgData.ItemsSource = result.DefaultView;

        }
        private void LoadDataToetsenbord()
        {
            QueryToetsenbord qt = new QueryToetsenbord();
            var result = qt.ViewToetsenbordDetails();
            DgData.ItemsSource = result.DefaultView;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Owner.Visibility = Visibility.Visible;
            WpfKeuzeScherm wpfk = new WpfKeuzeScherm(admin);
            wpfk.Show();

        }
        #endregion

        #region BUTTONCLICK_EVENTS
        private void BtnBewerken_Click(object sender, RoutedEventArgs e)
        {
            if (DgData.SelectedIndex > -1)
            {
                Edit();
            }
            else MessageBox.Show("Gelieve een rij te selecteren.");
        }
        private void DgData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DgData.SelectedIndex > -1)
            {
                Edit();
            }
            else MessageBox.Show("Gelieve een rij te selecteren.");

        }

        //Bewerken van een rij uit de databank
        private void Edit()
        {
            var row = (DataRowView)DgData.SelectedItem;
            var id = row[Productdetails.Database.Columns.Productnummer].ToString();
            var categorie = row[Productdetails.Database.Columns.Categorie].ToString();
            Headset h;
            Productdetails p = new Productdetails();
            int productnummer = int.Parse(row["productnummer"].ToString());

            //We checken de categorie, afhankelijk daarvan worden we doorgestuurd naar het volgende scherm
            if (categorie == "headset")
            {
                h = new Headset();
                p = new Productdetails();

                WpfHeadsetBewerken wpfhb = new WpfHeadsetBewerken(productnummer, admin);
                wpfhb.Owner = this;
                this.Visibility = Visibility.Hidden;
                wpfhb.Show();
            }
            else if (categorie == "laptop")
            {

                WpfLaptopBewerken wpflb = new WpfLaptopBewerken(productnummer, admin);
                wpflb.Owner = this;
                this.Visibility = Visibility.Hidden;
                wpflb.ShowDialog();
            }
            else if (categorie == "muis")
            {

                WpfMuisBewerken wpfmb = new WpfMuisBewerken(productnummer, admin);
                wpfmb.Owner = this;
                this.Visibility = Visibility.Hidden;
                wpfmb.ShowDialog();
            }
            else if (categorie == "toetsenbord")
            {
                WpfToetsenbordBewerken wpftb = new WpfToetsenbordBewerken(productnummer, admin);
                wpftb.Owner = this;
                this.Visibility = Visibility.Hidden;
                wpftb.ShowDialog();

            }
        }
        private void BtnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            WpfProductenToevoegen wpf = new WpfProductenToevoegen(admin);

            wpf.Owner = this;
            this.Visibility = Visibility.Hidden;
            wpf.ShowDialog();
        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }
        //Inlezen bestand
        private void BtnReadFile_Click(object sender, RoutedEventArgs e)
        {
            int aantalRijen = 0;
            var result = FileHelper.OpenFile(ref aantalRijen);
            if (aantalRijen != 0)
            {
                MessageBox.Show($"Er werden {aantalRijen} nieuwe rijen toegevoegd.");
            }

            else MessageBox.Show("Er werden geen nieuwe rijen toegevoegd. Kijk of het CSV-bestand wel klopt, en of het al niet eerder werd ingelezen.");

            LoadData(); //Nakijken, werkt niet 100%
        }
        //Exporteren bestand
        private void BtnExporteer_Click(object sender, RoutedEventArgs e)
        {
            //we stellen de data samen in de filehelperklasse
            string output = FileHelper.ConvertDataForFile();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Productgegevens_Beltech";
            if (sfd.ShowDialog() == true)
            {
                //Het exporteren zelf
                FileHelper.VerwerkBestand(sfd.FileName, output);
            }

        }
        private void BtnRijWissen_Click(object sender, RoutedEventArgs e)
        {
            //Even nakijken of we maar 1 item geselecteerd hebben
            if (DgData.SelectedItems.Count > 1)
                MessageBox.Show("Gelieve slechts 1 item te selecteren.");

            if (DgData.SelectedIndex > -1)
            {
                var row = (DataRowView)DgData.SelectedItem;
                string constraint = "";
                string tabelnaam = "";

                var id = int.Parse(row[Productdetails.Database.Columns.Productnummer].ToString());
                string categorie = row[Productdetails.Database.Columns.Categorie].ToString();
                switch (categorie)
                {

                    //We moeten in de verschillende tabelnamen de foreign key constraints verwijderen en terug aanmaken
                    case "headset":
                        {
                            constraint = "hd_pn_fk";
                            tabelnaam = "tblheadsetdetails";
                            break;
                        }
                    case "muis":
                        {
                            constraint = "md_pn_fk";
                            tabelnaam = "tblmuizendetails";
                            break;
                        }
                    case "laptop":
                        {
                            constraint = "ld_pn_fk";
                            tabelnaam = "tbllaptopDetails";
                            break;
                        }
                    case "toetsenbord":
                        {
                            constraint = "td_pn_fk";
                            tabelnaam = "tbltoetsenborddetails";
                            break;
                        }

                }
                var dialogresult = MessageBox.Show($"Ben u zeker dat u product {row[Productdetails.Database.Columns.Productnummer].ToString()} wilt verwijderen?", "Rij verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogresult == MessageBoxResult.Yes)
                {

                    QueryProductDetails qpd = new QueryProductDetails();
                    var result = qpd.DeleteRecord(id, constraint, tabelnaam);
                    Load();
                    ResetAfbeeldingen();
                    
                }
            }
            else MessageBox.Show("Gelieve een item te selecteren.");

        }
        private void BtnLeegmaken_Click(object sender, RoutedEventArgs e)
        {
            //Eventueel nog een inputbox voor schrijven voor het wachtwoord nogmaals in te geven

            var dialogresult = MessageBox.Show($"Ben u zeker dat u de databank wilt wissen?", "Verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogresult == MessageBoxResult.Yes)
            {
                QueryProductDetails qpd = new QueryProductDetails();
                var result = qpd.DeleteAllRecords();

                LoadData();
                CmbCategorie.SelectedItem = CmbAlle;
                ResetAfbeeldingen();
            }
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region AFBEELDINGEN
        string[] pathArray;
        private void DgData_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            //Een rij wordt geselecteerd in het datagrid. Let op, de images die we nodig hebben staan hier niet in.
            var row = (DataRowView)DgData.SelectedItem;
            if (row != null)
            {
                //Van die rij nemen we het productnummer
                var id = int.Parse(row[Productdetails.Database.Columns.Productnummer].ToString());

                QueryProductDetails qpd = new QueryProductDetails();

                //Van dat productnummer zoeken we de Datarow
                var result = qpd.GetImages(id);

                //We krijgen 1 resultaat
                DataRow dr = result.Rows[0];

                //Daarvan nemen we de string images
                var imagesKolom = dr[Productdetails.Database.Columns.Images].ToString();

                //Zorgt voor het eerste deel van het pad uit de admintabel
                

                    ImageHelper.Path = admin.Afbeeldingen_wpf;

                    // de string wordt in een array gegoten, die we nog op andere plaatsen nodig hebben
                    pathArray = ImageHelper.GetVolledigPad(imagesKolom, admin);

                    if (pathArray.Length == 0)
                    {
                        ImgVoorbeeld.Source = null;
                    }
                    else if (pathArray.Length == 1)
                    {
                        if (File.Exists(pathArray[0]))
                            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
                        else
                            ImgVoorbeeld.Source = null;

                    }
                    else
                    {
                        if (File.Exists(pathArray[1]))
                            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
                        else
                            ImgVoorbeeld.Source = null;
                    }

                    // we bepalen hoeveel strings we in onze array hebben, om te weten hoeveel images we moeten opvullen (0 - 7)
                    switch (pathArray.Length)
                    {
                        case 0:
                            MessageBox.Show("Geen rij geselecteerd.");
                            break;
                        case 1:
                            Show_1_Afbeelding(pathArray);
                            break;
                        case 2:
                            Show_2_Afbeeldingen(pathArray);
                            break;
                        case 3:
                            Show_3_Afbeeldingen(pathArray);
                            break;
                        case 4:
                            Show_4_Afbeeldingen(pathArray);
                            break;
                        case 5:
                            Show_5_Afbeeldingen(pathArray);
                            break;
                        case 6:
                            Show_6_Afbeeldingen(pathArray);
                            break;
                        case 7:
                            Show_7_Afbeeldingen(pathArray);
                            break;
                        default:
                            Show_7_Afbeeldingen(pathArray);
                            break;
                    }
               
            }

        }
        private void ResetAfbeeldingen()
        {
            ImgVoorbeeld.Source = null;
            Img1.Content = null;
            Img2.Content = null;
            Img3.Content = null;
            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;
        }



        #region BUTTONCLICKEVENTS VAN IMAGES
        private void Show_1_Afbeelding(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            Img2.Content = null;
            Img3.Content = null;
            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;

        }
        private void Show_2_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            Img3.Content = null;
            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;

        }
        private void Show_3_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;


            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;
        }
        private void Show_4_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;


            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;
        }
        private void Show_5_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;

            if (File.Exists(pathArray[4]))
                Img5.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img5.Content = null;

            Img6.Content = null;
            Img7.Content = null;
        }
        private void Show_6_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;

            if (File.Exists(pathArray[4]))

                Img5.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img5.Content = null;

            if (File.Exists(pathArray[5]))
                Img6.Content = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
            else
                Img6.Content = null;

            Img7.Content = null;
        }
        private void Show_7_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;

            if (File.Exists(pathArray[4]))

                Img5.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img5.Content = null;

            if (File.Exists(pathArray[5]))
                Img6.Content = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
            else
                Img6.Content = null;

            if (File.Exists(pathArray[6]))
                Img7.Content = new BitmapImage(new Uri(pathArray[6], UriKind.RelativeOrAbsolute));

            else
                Img7.Content = null;
        }

        private void Img1_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
        }

        private void Img2_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
        }

        private void Img3_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
        }

        private void Img4_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
        }

        private void Img5_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
        }

        private void Img6_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
        }

        private void Img7_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[6], UriKind.RelativeOrAbsolute));
        }
        #endregion
        #endregion
    }
}
