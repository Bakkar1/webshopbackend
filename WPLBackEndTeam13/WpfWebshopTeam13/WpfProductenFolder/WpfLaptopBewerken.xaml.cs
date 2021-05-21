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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Tools;



namespace WpfWebshopTeam13
{
    /// <summary>
    /// Interaction logic for WpfLaptopBewerken.xaml
    /// </summary>
    public partial class WpfLaptopBewerken : Window
    {
        private int productid;
        Laptop l = new Laptop();
        Productdetails p = new Productdetails();
        Admin admin = new Admin();
        SolidColorBrush rood = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD6D6"));
        public WpfLaptopBewerken(int productnummer, Admin a)
        {
            InitializeComponent();
            productid = productnummer;
            admin = a;
        }

        private void BtnLaptopBewerken_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTekstvakken())
            {
                try
                {
                    l.Update();
                    p.Update();
                    MessageBox.Show("Uw wijzigingen werden opgeslagen.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

         private bool ValidateTekstvakken()
        {
            bool typ, prs, krtng, kleinebeschr, kleurke, vrrd, imgs, valideren, os, processor, geheugen, prijzen,
                opslagruimte, scherm, toetsenbord, connectiviteit, accu, touchpad, poorten, audio, bijkomende, finish, afmetingen, gewicht, videokaart;
        float pr = 0;
        float kr = 0;
        valideren = false;
            if (ValidatieHelper.VType(TxtProducttypeLaptops.Text) && TxtProducttypeLaptops.Text != "")
            {
                p.Type = TxtProducttypeLaptops.Text;
                typ = true;
                TxtProducttypeLaptops.Background = Brushes.Transparent;
            }
            else
            {
                typ = false;
                TxtProducttypeLaptops.Background = rood;
            }



            krtng = false;
            prijzen = false;

            if (TxtPrijsLaptops.Text != "" && float.TryParse(TxtPrijsLaptops.Text, out pr) && pr >= 0)
            {
                TxtPrijsLaptops.Background = Brushes.Transparent;
                prs = true;
                if (TxtKortingLaptops.Text != "" && float.TryParse(TxtKortingLaptops.Text, out kr) && kr >= 0)
                {
                    krtng = true;
                    TxtKortingLaptops.Background = Brushes.Transparent;
                    if (pr > kr)
                    {
                        prijzen = true;
                        p.Prijs = pr;
                        p.Korting = kr;
                        TxtPrijsLaptops.Background = Brushes.White;
                        TxtKortingLaptops.Background = Brushes.White;
                    }
                    else
                    {
                        prs = false;
                        krtng = false;
                        TxtPrijsLaptops.Background = rood;
                        TxtKortingLaptops.Background = rood;
                    }
                }
                else
                {
                    krtng = false;
                    TxtKortingLaptops.Background = rood;
                }
            }
            else
            {
                TxtPrijsLaptops.Background = rood;
                prs = false;
            }

            if (TxtKleineBeschrijvingLaptops.Text != "")
            {
                kleinebeschr = true;
                p.Beschrijving = TxtKleineBeschrijvingLaptops.Text;
                TxtKleineBeschrijvingLaptops.Background = Brushes.Transparent;
            }
            else
            {
                kleinebeschr = false;
                TxtKleineBeschrijvingLaptops.Background = rood;
            }
            
            int vrd;
            if (TxtVoorraadLaptops.Text != "" && int.TryParse(TxtVoorraadLaptops.Text, out vrd) && vrd > -1)
            {
                TxtVoorraadLaptops.Background = Brushes.Transparent;
                p.Voorraad = vrd;
                vrrd = true;
            }
            else
            {
                TxtVoorraadLaptops.Background = rood;
                vrrd = false;
            }
            if (TxtKleurLaptops.Text != "")
            {
                TxtKleurLaptops.Background = Brushes.Transparent;
                kleurke = true;
                p.Kleur = TxtKleurLaptops.Text;
            }
            else
            {
                TxtKleurLaptops.Background = rood;
                kleurke = false;
            }


            if (TxtImagesLaptops.Text != "")
            {
                p.Images = TxtImagesLaptops.Text;
                imgs = true;
                TxtImagesLaptops.Background = Brushes.Transparent;

            }
            else
            {
                imgs = false;
                TxtImagesLaptops.Background = rood;
            }
            if(TxtOS.Text != "")
            {
                os = true;
                TxtOS.Background = Brushes.Transparent;
                l.Os = TxtOS.Text;
            }
            else
            {
                os = false;
                TxtOS.Background = rood;
            }
            
            if (TxtMemory.Text != "")
            {
                TxtMemory.Background = Brushes.Transparent;
                geheugen = true;
                l.Memory = TxtMemory.Text;
            }
            else
            {
                TxtMemory.Background = rood;
                geheugen = false;
            }
            if (TxtProcessor.Text != "")
            {
                TxtProcessor.Background = Brushes.Transparent;
                processor = true;
                l.Processor = TxtProcessor.Text;
            }
            else
            {
                TxtProcessor.Background = rood;
                processor = false;
            }
            if (TxtGraphics.Text != "")
            {
                TxtGraphics.Background = Brushes.Transparent;
                videokaart = true;
                l.Graphics = TxtKleurLaptops.Text;
            }
            else
            {
                TxtGraphics.Background = rood;
                videokaart = false;
            }
            if (TxtStorage.Text != "")
            {
                TxtStorage.Background = Brushes.Transparent;
                opslagruimte = true;
                l.Storage = TxtKleurLaptops.Text;
            }
            else
            {
                TxtStorage.Background = rood;
                opslagruimte = false;
            }
            if (TxtDisplay.Text != "")
            {
                TxtDisplay.Background = Brushes.Transparent;
                scherm = true;
                l.Display = TxtKleurLaptops.Text;
            }
            else
            {
                TxtDisplay.Background = rood;
                scherm = false;
            }
            if (TxtKeyboard.Text != "")
            {
                TxtKeyboard.Background = Brushes.Transparent;
               toetsenbord = true;
                l.Keyboard = TxtKeyboard.Text;
            }
            else
            {
                TxtKeyboard.Background = rood;
                toetsenbord = false;
            }
            if (TxtConnectivity.Text != "")
            {
                TxtConnectivity.Background = Brushes.Transparent;
                connectiviteit = true;
                l.Connectivity = TxtConnectivity.Text;
            }
            else
            {
                TxtConnectivity.Background = rood;
                connectiviteit = false;
            }
            if (TxtBattery_and_adaptor.Text != "")
            {
                TxtBattery_and_adaptor.Background = Brushes.Transparent;
                accu = true;
                l.Battery_and_adaptor = TxtBattery_and_adaptor.Text;
            }
            else
            {
                TxtBattery_and_adaptor.Background = rood;
                accu = false;
            }
            if (TxtTouchpad.Text != "")
            {
                TxtTouchpad.Background = Brushes.Transparent;
                touchpad = true;
                l.Touchpad = TxtTouchpad.Text;
            }
            else
            {
                TxtTouchpad.Background = rood;
                touchpad = false;
            }
            if (TxtInput_and_output.Text != "")
            {
                TxtInput_and_output.Background = Brushes.Transparent;
                poorten = true;
                l.Input_and_output = TxtInput_and_output.Text;
            }
            else
            {
                TxtInput_and_output.Background = rood;
                poorten = false;
            }
            if (TxtAudio.Text != "")
            {
                TxtAudio.Background = Brushes.Transparent;
                audio = true;
                l.Audio = TxtAudio.Text;
            }
            else
            {
                TxtAudio.Background = rood;
                audio = false;
            }
            if (TxtAdditional_features.Text != "")
            {
                TxtAdditional_features.Background = Brushes.Transparent;
                bijkomende = true;
                l.Additional_features = TxtAdditional_features.Text;
            }
            else
            {
                TxtAdditional_features.Background = rood;
                bijkomende = false;
            }
            if (TxtFinish.Text != "")
            {
                TxtFinish.Background = Brushes.Transparent;
                finish = true;
                l.Finish = TxtFinish.Text;
            }
            else
            {
                TxtFinish.Background = rood;
                finish = false;
            }
            if (TxtDimensions.Text != "")
            {
                TxtDimensions.Background = Brushes.Transparent;
                afmetingen = true;
                l.Dimensions = TxtDimensions.Text;
            }
            else
            {
                TxtDimensions.Background = rood;
                afmetingen = false;
            }
            if (TxtWeightlp.Text != "")
            {
                TxtWeightlp.Background = Brushes.Transparent;
                gewicht = true;
                l.Weight = TxtWeightlp.Text;
            }
            else
            {
                TxtWeightlp.Background = rood;
                gewicht = false;
            }

            if (typ == true && processor == true && videokaart == true && os == true && opslagruimte == true && prs == true && krtng == true && kleinebeschr == true &&
                kleurke == true && vrrd == true && imgs == true && scherm == true && toetsenbord == true && connectiviteit == true && accu == true && geheugen == true
                && poorten == true && touchpad == true && audio == true && bijkomende == true && finish == true && afmetingen == true && gewicht == true && prijzen == true)
            {
                valideren = true;
            }
            return valideren;
        }
        


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryLaptop ql = new QueryLaptop();
            DataTable result = ql.getSingelJoin(productid);
            DataRow dr = result.Rows[0];
            UpdateFormLaptops(GetProductDetails(dr), GetLaptop(dr));
        }
        private Productdetails GetProductDetails(DataRow dr)
        {
            p.Productnummer = productid;
            p.Type = dr[Productdetails.Database.Columns.Type].ToString();
            p.Prijs = float.Parse(dr[Productdetails.Database.Columns.Prijs].ToString());
            p.Korting = float.Parse(dr[Productdetails.Database.Columns.Korting].ToString());
            p.Beschrijving = dr[Productdetails.Database.Columns.Beschrijving].ToString();
            p.Uitgebreide_beschrijving = dr[Productdetails.Database.Columns.Uitgebreide_beschrijving].ToString();
            p.Kleur = dr[Productdetails.Database.Columns.Kleur].ToString();
            p.Voorraad = int.Parse(dr[Productdetails.Database.Columns.Voorraad].ToString());
            p.Images = dr[Productdetails.Database.Columns.Images].ToString();
            p.Categorie = dr[Productdetails.Database.Columns.Categorie].ToString();
            return p;
        }
        private Laptop GetLaptop(DataRow dr)
        {
            l.LaptopId = int.Parse(dr[Laptop.Database.Columns.LaptopID].ToString());
            l.Os = dr[Laptop.Database.Columns.Os].ToString();
            l.Processor = dr[Laptop.Database.Columns.Processor].ToString();
            l.Memory = dr[Laptop.Database.Columns.Memory].ToString();
            l.Graphics = dr[Laptop.Database.Columns.Graphics].ToString();
            l.Storage = dr[Laptop.Database.Columns.Storage].ToString();
            l.Display = dr[Laptop.Database.Columns.Display].ToString();
            l.Keyboard = dr[Laptop.Database.Columns.Keyboard].ToString();
            l.Connectivity = dr[Laptop.Database.Columns.Connectivity].ToString();
            l.Battery_and_adaptor = dr[Laptop.Database.Columns.Battery_and_adaptor].ToString();
            l.Touchpad = dr[Laptop.Database.Columns.Touchpad].ToString();
            l.Input_and_output = dr[Laptop.Database.Columns.Input_and_output].ToString();
            l.Audio = dr[Laptop.Database.Columns.Audio].ToString();
            l.Additional_features = dr[Laptop.Database.Columns.Additional_features].ToString();
            l.Finish = dr[Laptop.Database.Columns.Finish].ToString();
            l.Dimensions = dr[Laptop.Database.Columns.Dimensions].ToString();
            l.Weight = dr[Laptop.Database.Columns.Weight].ToString();

            return l;
        }
        private void UpdateFormLaptops(Productdetails p, Laptop l)
        {

            TxtProducttypeLaptops.Text = p.Type;
            TxtPrijsLaptops.Text = p.Prijs.ToString();
            TxtKleineBeschrijvingLaptops.Text = p.Beschrijving;
            TxtKortingLaptops.Text = p.Korting.ToString();
            TxtUitgebreideBeschrijvingLaptops.Text = p.Uitgebreide_beschrijving;
            TxtKleurLaptops.Text = p.Kleur;
            TxtVoorraadLaptops.Text = p.Voorraad.ToString();
            TxtImagesLaptops.Text = p.Images;
            

            TxtOS.Text = l.Os;
            TxtProcessor.Text = l.Processor;
            TxtMemory.Text = l.Memory;
            TxtGraphics.Text = l.Graphics;
            TxtStorage.Text = l.Storage;
            TxtDisplay.Text = l.Display;
            TxtKeyboard.Text = l.Keyboard;
            TxtConnectivity.Text = l.Connectivity;
            TxtBattery_and_adaptor.Text = l.Battery_and_adaptor;
            TxtTouchpad.Text = l.Touchpad;
            TxtInput_and_output.Text = l.Input_and_output;
            TxtAudio.Text = l.Audio;
            TxtAdditional_features.Text = l.Additional_features;
            TxtFinish.Text = l.Finish;
            TxtDimensions.Text = l.Dimensions;
            TxtWeightlp.Text = l.Weight;
            UpdateAfbeeldingen(TxtImagesLaptops.Text, admin);
        }
        

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBlader_Click(object sender, RoutedEventArgs e)
        {
            // We hebben de admin nodig omdat zijn pad uniek is aan zijn computer

            if (TxtImagesLaptops.Text == "")
            {
                TxtImagesLaptops.Text = $"{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingen(TxtImagesLaptops.Text, admin);
            }
            else
            {
                TxtImagesLaptops.Text += $"|{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingen(TxtImagesLaptops.Text, admin);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
        }
        #region afbeeldingen
        string[] pathArray;
        private void UpdateAfbeeldingen(string inhoud, Admin admin)
        {

            ImageHelper.Path = admin.Afbeeldingen_wpf;
            pathArray = ImageHelper.GetVolledigPad(inhoud, admin);



            if (pathArray.Length == 0)
            {
                ImgVoorbeeldl.Source = null;
            }
            else if (pathArray.Length == 1)
            {
                if (File.Exists(pathArray[0]))
                    ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeldl.Source = null;

            }
            else
            {
                if (File.Exists(pathArray[1]))
                    ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeldl.Source = null;
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
        #region BUTTONCLICKEVENTS VAN IMAGES
        private void Show_1_Afbeelding(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img8.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            Img9.Content = null;
            Img10.Content = null;
            Img11.Content = null;
            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;

        }
        private void Show_2_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img8.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArray[1]))
                Img9.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            Img10.Content = null;
            Img11.Content = null;
            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;

        }
        private void Show_3_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img8.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArray[1]))
                Img9.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArray[2]))
                Img10.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;


            Img11.Content = null;
            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;
        }
        private void Show_4_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img8.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArray[1]))
                Img9.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArray[2]))
                Img10.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArray[3]))
                Img11.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;


            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;
        }
        private void Show_5_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img8.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArray[1]))
                Img9.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArray[2]))
                Img10.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArray[3]))
                Img11.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;

            if (File.Exists(pathArray[4]))
                Img12.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img12.Content = null;

            Img13.Content = null;
            Img14.Content = null;
        }
        private void Show_6_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img8.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArray[1]))
                Img9.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArray[2]))
                Img10.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArray[3]))
                Img11.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;

            if (File.Exists(pathArray[4]))

                Img12.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img12.Content = null;

            if (File.Exists(pathArray[5]))
                Img13.Content = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
            else
                Img13.Content = null;

            Img14.Content = null;
        }
        private void Show_7_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img8.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArray[1]))
                Img9.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArray[2]))
                Img10.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArray[3]))
                Img11.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;

            if (File.Exists(pathArray[4]))

                Img12.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img12.Content = null;

            if (File.Exists(pathArray[5]))
                Img13.Content = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
            else
                Img13.Content = null;

            if (File.Exists(pathArray[6]))
                Img14.Content = new BitmapImage(new Uri(pathArray[6], UriKind.RelativeOrAbsolute));

            else
                Img14.Content = null;
        }

        private void Img8_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
        }

        private void Img9_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
        }

        private void Img10_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
        }

        private void Img11_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
        }

        private void Img12_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
        }

        private void Img13_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
        }

        private void Img14_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeldl.Source = new BitmapImage(new Uri(pathArray[6], UriKind.RelativeOrAbsolute));
        }


        #endregion

        #endregion

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateAfbeeldingen(TxtImagesLaptops.Text, admin);
            ValidateTekstvakken();
        }
    }
}
